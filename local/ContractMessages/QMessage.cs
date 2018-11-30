namespace ContractMessages
{
    public class QMessage
    {
        public string Name { get; }
        
        public string Message { get; }

        public QMessage(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}