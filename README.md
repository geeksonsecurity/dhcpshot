# dhcpshot

Use [DotNetProjects.DhcpServer](https://www.nuget.org/packages/DotNetProjects.DhcpServer/) library to quickly create a predefined DHCP Server answering with a fixed IP to request coming from the interface specified by the user. E.g.: you connect your RPI in DHCP mode directly to your laptop's ethernet interface.

## Publish

Generate self-contained executable for all platforms.
```
dotnet publish -r win10-x64 -c release /p:PublishSingleFile=true
dotnet publish -r osx-x64 -c release /p:PublishSingleFile=true
dotnet publish -r linux-x64 -c release /p:PublishSingleFile=true
```