using Akka.Actor;

namespace HostApp
{
    public class MessageActor : ReceiveActor
    {
        private readonly IMessageService _messageService;

        public MessageActor(IMessageService messageService)
        {
            _messageService = messageService;

            Start();
        }


        private void Start()
        {
            Receive<string>(message =>
            {
                _messageService.Out(message);
            });

        }
    }
}