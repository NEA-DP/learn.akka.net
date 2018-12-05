﻿using System;
using Akka.Actor;
using Akka.Configuration;

namespace HostAkkaSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("DeployTarget", ConfigurationFactory.ParseString(@"
            akka {  
                actor.provider = remote
                remote {
                    dot-netty.tcp {
                        port = 8090
                        hostname = localhost
                    }
                }
            }")))
            {
                Console.ReadKey();
            }
        }
    }
}