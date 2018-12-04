
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Configuration;
using SharedLogger;
using SharedMessages;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonConsoleLog.Configure();
            
            var container = AutofacComposition.Load();
            
            var akkaConfig = ConfigurationFactory.ParseString(File.ReadAllText("akkaConfig.hocon"));
            
            var system = ActorSystem.Create("localsystem", akkaConfig);
            system.UseAutofac(container);


      
            
            var actors = ImmutableHashSet.Create<ActorPath>(ActorPath.Parse("akka.tcp://ClusterSystem@localhost:51111/system/receptionist"));


            var c = system.ActorOf(ClusterClient.Props(
                ClusterClientSettings.Create(system).WithInitialContacts(actors)), "client");
            
            //c.Tell(new ClusterClient.Send("/user/EchoResponser", new Echo("aaaa")));
            
            

            
            c.Ask(new ClusterClient.Send("/user/EchoResponser", new Echo("aaaa"))).ContinueWith(t =>
            {
                if (t.Result is EchoResponse)
                {
                    var r = t.Result as EchoResponse;
                    system.Log.Debug(r.Message);
                }

            }).Wait();

            Console.ReadLine();



        }
    }
}