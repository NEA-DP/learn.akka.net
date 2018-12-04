using Akka.Actor;
using Akka.Event;
using SharedMessages;

namespace SomeNode.Actors
{
    public class EchoResponserActor : ReceiveActor
    {
        public EchoResponserActor()
        {
            Receive<Echo>(echo =>
            {
                Context.GetLogger().Debug($"Incoming echo : {echo.Message}");
                Sender.Tell(new EchoResponse($"QQ : {echo.Message}"));
            });
        }
    }
}