using System;

namespace HostApp
{
    public class ConsoleMessageService : IMessageService
    {
        public void Out(string message)
        {
            Console.WriteLine(message);
        }
    }
}