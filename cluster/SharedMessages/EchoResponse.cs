namespace SharedMessages
{
    public class EchoResponse
    {
        public string Message { get; }

        public EchoResponse(string message)
        {
            Message = message;
        }
    }
}