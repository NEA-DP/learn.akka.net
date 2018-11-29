using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("localakkasystem"))
            {
                system.UseAutofac(AutofacConfig.Init());
                
                var actorProps = system.DI().Props<MessageActor>();

                var actorRef = system.ActorOf(actorProps, "MessageActor");
                
                actorRef.Tell("qq");
                actorRef.Tell("qq");
                actorRef.Tell("qq");

                Console.ReadLine();
            }
        }
    }
}