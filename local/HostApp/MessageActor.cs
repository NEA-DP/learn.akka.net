using System;
using Akka.Actor;
using Akka.Event;

namespace HostApp
{
    public class MessageActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        
        private readonly IMessageService _messageService;

        private int _messagePrefix = 0;

        private MessageActorState _state;

        public MessageActor(IMessageService messageService)
        {
            _messageService = messageService;
            

            Starting();
        }
        
        
        private void Starting()
        {
            _state = MessageActorState.NotInitialized;
            
            Receive<MessageActorConfigureMessage>(config =>
            {
                BecomeWork(config.MessagePrefix);
            });
            Receive<string>(message =>
            {
                Stash.Stash();
                Console.WriteLine($"Message '{message}' stashed.");
            });
            Receive<MessageActorGetStateMessage>(message =>
            {
                Context.System.ActorSelection("*/MessageActorInitializerActor").Tell(new MessageActorCurrentStateMessage(_messagePrefix, _state));
            });
        }
        
        private void BecomeWork(int messagePrefix)
        {
            _messagePrefix = messagePrefix;
            _state = MessageActorState.Initialized;
            Become(Work);
            Stash.UnstashAll();
        }


        private void Work()
        {
            Receive<MessageActorGetStateMessage>(message =>
            {
                Context.System.ActorSelection("*/MessageActorInitializerActor").Tell(new MessageActorCurrentStateMessage(_messagePrefix, _state));
            });
            Receive<string>(message =>
            {
                _messageService.Out($"_{_messagePrefix}_ : {message}");
            });

        }

        protected override void PostStop()
        {
            Context.System.ActorSelection("*/MessageActorInitializerActor").Tell(new MessageActorCurrentStateMessage(_messagePrefix, _state));
            base.PostStop();
        }
    }
}