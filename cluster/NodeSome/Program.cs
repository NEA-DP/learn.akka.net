using System;
using Core;

namespace NodeSome
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogConfig.InitConsoleLogger();
        
            var service = new NodeSomeService();
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