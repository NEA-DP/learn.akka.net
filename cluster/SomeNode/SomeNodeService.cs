using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Configuration;
using Akka.DI.Core;
using SharedActors;
using SomeNode.Actors;

namespace SomeNode
{
    public class SomeNodeService
    {
        protected ActorSystem System;

        public Task WhenTerminated => System.WhenTerminated;


        public bool Start()
        {
            var akkaConfig = ConfigurationFactory.ParseString(File.ReadAllText("akkaConfig.hocon"));
            
            var container = AutofacComposition.Load();
            
            System = ActorSystem.Create("ClusterSystem", akkaConfig);
            
            System.UseAutofac(container);
                
                
            System.ActorOf(System.DI().Props<ClusterListener>(), "ClusterListener");
            
            var x = System.ActorOf(System.DI().Props<EchoResponserActor>(), "EchoResponser");
            var xx = ClusterClientReceptionist.Get(System);
            xx.RegisterService(x);    
                

            return true;
        }

        public Task Stop()
        {
            return CoordinatedShutdown.Get(System).Run(CoordinatedShutdown.ClusterDowningReason.Instance);
        }
    }
}