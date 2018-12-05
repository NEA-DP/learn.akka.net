using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Autofac;
using Core;
using SharedActors;

namespace NodeSeed
{
    public class NodeSeedService
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
            
            System.ActorOf(System.DI().Props<DeviceEventsReceiverActor>(), Const.Akka.SharedActorsNames.DeviceEventsReceiver);
                
            return true;
        }

        public Task Stop()
        {
            return CoordinatedShutdown.Get(System).Run(CoordinatedShutdown.ClusterDowningReason.Instance);
        }
    }
}