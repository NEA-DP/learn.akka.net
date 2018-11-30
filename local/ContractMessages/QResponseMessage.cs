namespace ContractMessages
{
    public class QResponseMessage
    {
        public string Message { get; }

        public QResponseMessage(string message)
        {
            Message = message;
        }
    }
}