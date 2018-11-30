using System;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Akka.Routing;
using Akka.Util.Internal;
using ContractMessages;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";

            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            LogManager.Configuration = config;


            
            var myConfig = ConfigurationFactory.ParseString(@"
akka {  
    loglevel = ""DEBUG""
    loggers=[""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
    actor {
        provider = remote
    }
    remote {
        dot-netty.tcp {
		    port = 0
		    hostname = localhost
        }
log-remote-lifecycle-events = DEBUG
    }
}
");
            using (var system = ActorSystem.Create("localakkasystem",  myConfig))
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