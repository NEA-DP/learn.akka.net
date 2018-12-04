using System;
using SharedLogger;

namespace SomeNode
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonConsoleLog.Configure();
            
            var service = new SomeNodeService();
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