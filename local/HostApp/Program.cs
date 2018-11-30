﻿using System;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Routing;
using Akka.Util.Internal;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace HostApp
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
            port = 51111
            hostname = localhost
        }

    }
}
");
            
            using (var system = ActorSystem.Create("hostappakkasystem",  myConfig))
            {
                system.UseAutofac(AutofacConfig.Init());
                
                
                system.ActorOf(system.DI().Props<MessageActorInitializerActor>(), "MessageActorInitializerActor");
                
                
                var router = system.ActorOf(system.DI().Props<MessageActor>().WithRouter(new RoundRobinPool(5)), "messagePool");


                router.Tell(new Broadcast(new MessageActorGetStateMessage()));
                

                Console.ReadLine();
                
                
                router.Tell(new Broadcast(PoisonPill.Instance));
                
                
                Console.ReadLine();
            }
        }
    }
}