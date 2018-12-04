using System;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Akka.Routing;
using Akka.Util.Internal;
using Autofac;
using ContractMessages;
using Core;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.Init();
            
            NLogConfig.Init();
        

            var akkaConfig = container.Resolve<IHoconFileLoader>().ParseConfig("akkaConfig.hocon");

            using (var system = ActorSystem.Create("localakkasystem",  akkaConfig))
            {
                system.UseAutofac(AutofacConfig.Init());
                
                
                var localActor = system.ActorOf(system.DI().Props<LocalActor>(), "LocalActor");
                
                
                Enumerable.Range(0, 50).AsParallel().ForEach(i =>
                {
                    localActor.Tell(new QMessage("ClientApp", "Hi"));
                });
                
                Console.ReadLine();
            }
        }
    }
}