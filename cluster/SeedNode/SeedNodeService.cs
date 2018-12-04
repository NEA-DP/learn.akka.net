using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Akka.Routing;
using Autofac;
using SharedActors;

namespace SeedNode
{
    public class SeedNodeService
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
                
                
                

            return true;
        }

        public Task Stop()
        {
            return CoordinatedShutdown.Get(System).Run(CoordinatedShutdown.ClusterDowningReason.Instance);
        }
    }
}