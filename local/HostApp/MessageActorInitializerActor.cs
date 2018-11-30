using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Event;

namespace HostApp
{
    public class MessageActorInitializerActor : ReceiveActor
    {
        private readonly HashSet<int> _initializedActors = new HashSet<int>();
        
        public MessageActorInitializerActor()
        {
            Receive<MessageActorCurrentStateMessage>(message =>
            {
                switch (message.State)
                {
                    case MessageActorState.NotInitialized:
                        if (!IsInitialized(message.Prefix))
                        {
                            Initialize(Context.Sender);
                        }
                        break;
                    case MessageActorState.Initialized:
                        if (IsInitialized(message.Prefix))
                        {
                            Deinitialize(message.Prefix);
                        }
                        break;
                }
            });
        }


        private bool IsInitialized(int prefix)
        {
            return _initializedActors.Contains(prefix);
        }
        
        
        private void Initialize(IActorRef actor)
        {
            var idx = _initializedActors.Count + 1;
            
            _initializedActors.Add(idx);
            
            actor.Tell(new MessageActorConfigureMessage(idx));
        }

        private void Deinitialize(int prefix)
        {
            _initializedActors.Remove(prefix);
            Context.GetLogger().Debug($"Deinitialize actor with prefix {prefix}");
        }
    }
}