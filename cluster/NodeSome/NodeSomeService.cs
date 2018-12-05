using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Autofac;
using Core;
using SharedActors;

namespace NodeSome
{
    public class NodeSomeService
    {
        protected ActorSystem System;

        public Task WhenTerminated => System.WhenTerminated;


        public bool Start()
        {
            var container = AutofacComposition.Build();

            var configReader = container.Resolve<IHoconConfigLoader>();
            
            var akkaConfig = configReader.ParseConfig(Const.Akka.DefaultConfigFileName);
            
            System = ActorSystem.Create(Const.Akka.ClusterSystemName, akkaConfig);
            
            System.UseAutofac(container);
                
            System.ActorOf(System.DI().Props<ClusterListener>(), Const.Akka.SharedActorsNames.ClusterListener);
                
            return true;
        }

        public Task Stop()
        {
            return CoordinatedShutdown.Get(System).Run(CoordinatedShutdown.ClusterDowningReason.Instance);
        }
    }
}