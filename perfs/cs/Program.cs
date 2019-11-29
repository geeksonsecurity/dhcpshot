using System;

namespace perfs
{
    class Program
    {
        private static readonly Random random = new Random();

        static long Sum(long a, long b)
        {
            return a + b + random.Next();
        }
        static void Main(string[] args)
        {
            long sum = 0;
            for (long count = 0; count < Math.Pow(10, 8); count++)
            {
                sum = Sum(sum, count);
            }
        }
    }
}
