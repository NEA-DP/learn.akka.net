using System;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Routing;
using Akka.Util.Internal;
using Autofac;
using Core;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogConfig.Init();
        
            var service = new HostService();
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