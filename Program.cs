using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using DotNetProjects.DhcpServer;

namespace dhcpshot
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option(
                    new string[] { "--interface-ip", "-i" },
                    "IPv4 for the interface we are going to listen to")
                {
                    Argument = new Argument<string>{ Arity = ArgumentArity.ExactlyOne },
                    Required = true
                },
                new Option(
                    new string[] { "--target-ip", "-t" },
                    "IPv4 that will be given to the DHCP client, if none given it will be generated from interface IP")
                {
                    Argument = new Argument<string>()
                }
            };
            rootCommand.Description = "DHCPshot - Assign IP address to first client asking on the specified interface";
            rootCommand.Handler = CommandHandler.Create<string, string>(StartServer);
            rootCommand.InvokeAsync(args).Wait();
        }

        public static void StartServer(string interfaceIp, string targetIp)
        {
            Console.WriteLine($"Looking for interface for IP {interfaceIp}");
            var intf = NetworkInterface.GetAllNetworkInterfaces()
                .Where(
                    x => x.GetIPProperties().UnicastAddresses
                        .Any(y => y.Address.Equals(IPAddress.Parse(interfaceIp)))
                    ).FirstOrDefault();
            if (intf == null)
            {
                Console.WriteLine($"Interface for IP {interfaceIp} not found!");
                return;
            }
            Console.WriteLine($"Starting DHCP server on interface {intf.Name}");
            var server = new DHCPServer();

            if (targetIp == null)
            {
                var rnd = new Random();
                var ip_bytes = interfaceIp.Split(".");
                var lastByte = int.Parse(ip_bytes[3]);
                int newByte = rnd.Next(2, 254);
                while (newByte == lastByte)
                {
                    newByte = rnd.Next(2, 254);
                }
                ip_bytes[3] = newByte.ToString();
                targetIp = string.Join(".", ip_bytes);
            }
            Console.WriteLine($"Client will receive IP {targetIp}");
            server.OnDataReceived += delegate (DHCPRequest dhcpRequest)
            {
                Request(dhcpRequest, IPAddress.Parse(targetIp));
            };
            server.BroadcastAddress = IPAddress.Broadcast;
            server.SendDhcpAnswerNetworkInterface = intf;
            server.Start();
            Console.WriteLine("Running DHCP server... Press CTRL-C to exit");
        }

        static void Request(DHCPRequest dhcpRequest, IPAddress targetIp)
        {
            try
            {
                var type = dhcpRequest.GetMsgType();
                var replyOptions = new DHCPReplyOptions();
                replyOptions.SubnetMask = IPAddress.Parse("255.255.255.0");

                if (type == DHCPMsgType.DHCPDISCOVER)
                    dhcpRequest.SendDHCPReply(DHCPMsgType.DHCPOFFER, targetIp, replyOptions);
                if (type == DHCPMsgType.DHCPREQUEST)
                    dhcpRequest.SendDHCPReply(DHCPMsgType.DHCPACK, targetIp, replyOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}