using System;
using SharedLogger;

namespace SeedNode
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonConsoleLog.Configure();
            
            var service = new SeedNodeService();
            service.Start();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                
                service.Stop().Wait();

                
                eventArgs.Cancel = true;
            };

            service.WhenTerminated.Wait();
        }
    }
}