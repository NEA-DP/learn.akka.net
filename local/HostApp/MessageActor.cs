using Akka.Actor;

namespace HostApp
{
    public class MessageActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        
        private readonly IMessageService _messageService;

        private string _messagePrefix;

        public MessageActor(IMessageService messageService)
        {
            _messageService = messageService;

            Starting();
        }
        
        
        private void Starting()
        {
            Receive<MessageActorConfigureMessage>(config =>
            {
                BecomeWork(config.MessagePrefix);
            });
            Receive<string>(message =>
            {
                Stash.Stash();
            });
        }
        
        private void BecomeWork(string messagePrefix)
        {
            _messagePrefix = messagePrefix;
            Become(Work);
            Stash.UnstashAll();
        }


        private void Work()
        {
            Receive<string>(message =>
            {
                _messageService.Out($"{_messagePrefix}{message}");
            });

        }

        
    }
}