using System;
using Akka.Actor;
using Core;
using SharedActors.Messages;

namespace DeviceEventsDispatcher
{
    public class DeviceEventsDispatcherActor : ReceiveActor
    {
        private ICancelable _dispatchTask;

        public DeviceEventsDispatcherActor()
        {
            Receive<DeviceEventsDispatcherStartMessage>(sayHello =>
            {
                var receiveActor = Context.System.ActorSelection($"akka.tcp://ClusterSystem@localhost:51111/{Const.Akka.SharedActorsNames.DeviceEventsReceiver}");
                
                receiveActor.Tell(new DeviceEventMessage(Guid.NewGuid(), Guid.NewGuid().ToString()), ActorRefs.NoSender);
            });
        }

        protected override void PreStart()
        {
            _dispatchTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1), Context.Self, new DeviceEventsDispatcherStartMessage(), ActorRefs.NoSender);
        }

        protected override void PostStop()
        {
            _dispatchTask.Cancel();
        }
    }
}