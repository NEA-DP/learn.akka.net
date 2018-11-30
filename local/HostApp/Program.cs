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
                
                var msActor = system.ActorOf(system.DI().Props<MessageActor>(), "MessageActor");
                
                
                msActor.Tell("this message was stashed before the actor initialization 1");
                msActor.Tell("this message was stashed before the actor initialization 2");
                msActor.Tell("this message was stashed before the actor initialization 3");
                
                
                
                msActor.Tell(new MessageActorConfigureMessage("_PREFIX_"));
                
                
                msActor.Tell("this message 4");
                
                msActor.Tell("this message 5");

                Console.ReadLine();
            }
        }
    }
}