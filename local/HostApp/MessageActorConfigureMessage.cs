namespace HostApp
{
    public class MessageActorConfigureMessage
    {
        public MessageActorConfigureMessage(string messagePrefix)
        {
            MessagePrefix = messagePrefix;
        }

        public string MessagePrefix { get; }
    }
}