using System;
using Akka.Actor;
using ContractMessages;

namespace ClientApp
{
    public class LocalActor : ReceiveActor
    {
        public LocalActor()
        {
            Receive<QMessage>(message =>
            {
                var remoteActor = Context.ActorSelection("akka.tcp://hostappakkasystem@localhost:51111/user/messagePool");
               
                remoteActor.Tell(message, Self);
            });
            
            
            Receive<QResponseMessage>(message =>
            {
                Console.WriteLine(message.Message);
            });
        }
    }
}