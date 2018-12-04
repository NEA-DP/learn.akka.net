using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using Autofac;
using Core;
using Modules.HoconLoader;

namespace HostApp
{
    public class HostService
    {
        protected ActorSystem System;

        public Task WhenTerminated => System.WhenTerminated;

        private IActorRef GetActor(string actorRef)
        {
            return System.ActorSelection(actorRef).Anchor;
        }

        public bool Start()
        {
            
            var container = AutofacConfig.Init();
            
            var akkaConfig = container.Resolve<IHoconFileLoader>().ParseConfig("akkaConfig.hocon");

            System = ActorSystem.Create("hostappakkasystem", akkaConfig);
            
            System.UseAutofac(container);
                
                
            System.ActorOf(System.DI().Props<MessageActorInitializerActor>(), "MessageActorInitializerActor");
                
                
            var router = System.ActorOf(System.DI().Props<MessageActor>().WithRouter(new RoundRobinPool(5)), "messagePool");


            router.Tell(new Broadcast(new MessageActorGetStateMessage()));
                

            return true;
        }

        public Task Stop()
        {
            GetActor("messagePool").Tell(new Broadcast(PoisonPill.Instance));
            
            return CoordinatedShutdown.Get(System).Run(CoordinatedShutdown.ClusterDowningReason.Instance);
        }

    }
}