namespace HostApp
{
    public class MessageActorConfigureMessage
    {
        public MessageActorConfigureMessage(int messagePrefix)
        {
            MessagePrefix = messagePrefix;
        }

        public int MessagePrefix { get; }
    }
}