using System;

namespace SharedMessages
{
    public class Echo
    {
        public string Message { get; }

        public Echo(string message)
        {
            Message = message;
        }
    }
}