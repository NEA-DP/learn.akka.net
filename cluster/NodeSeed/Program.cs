using System;
using Core;

namespace NodeSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogConfig.InitConsoleLogger();
        
            var service = new NodeSeedService();
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