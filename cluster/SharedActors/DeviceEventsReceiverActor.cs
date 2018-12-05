using Akka.Actor;
using SharedActors.Messages;

namespace SharedActors
{
    public class DeviceEventsReceiverActor: ReceiveActor
    {
        public DeviceEventsReceiverActor()
        {
            Listen();
        }


        private void Listen()
        {
            Receive<DeviceEventMessage>(de =>
            {
                Context.System.Log.Debug($"Received | {de.Id} : {de.Value}");
            });
        }
    }
}