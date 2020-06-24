using System;
using System.Threading;

namespace Atlassian
{
    class Program
    {
        //[RateLimit("hello", 4, new timeStamp(100000)]
        static void Main(string[] args)
        {

            var rateLimiter = new RateLimiter
            {
                actionName = "main",
                interval = new TimeSpan(0, 0, 5),
                maxAllowed = 2,
                userId = "abhishek"
            };

            for(int i =0; i< 5; i++)
            {
                if (i >= 3)
                {
                    Thread.Sleep(6000);
                }
                Console.WriteLine($"Action : {i + 1}");
                var allowed = rateLimiter.Limit();
                Console.WriteLine(allowed);
            }

        }
    }
}
