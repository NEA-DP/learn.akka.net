namespace HostApp
{
    public class MessageActorCurrentStateMessage
    {
        public int Prefix { get; }
        
        public MessageActorState State { get; }

        public MessageActorCurrentStateMessage(int prefix, MessageActorState state)
        {
            Prefix = prefix;
            State = state;
        }
    }
}