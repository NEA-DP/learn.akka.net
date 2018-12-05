using System;
using Akka.Actor;
using Akka.Configuration;
using SharedAktors;

namespace ClientAkkaSystem
{
    class Program
{
    class SayHello { }

    class HelloActor : ReceiveActor
    {
        private IActorRef _remoteActor;
        private int _helloCounter;
        private ICancelable _helloTask;

        public HelloActor(IActorRef remoteActor)
        {
            _remoteActor = remoteActor;
            Receive<Hello>(hello =>
            {
                Console.WriteLine("Received {1} from {0}", Sender, hello.Message);
            });

            Receive<SayHello>(sayHello =>
            {
                _remoteActor.Tell(new Hello("hello"+_helloCounter++));
            });
        }

        protected override void PreStart()
        {
            _helloTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1), Context.Self, new SayHello(), ActorRefs.NoSender);
        }

        protected override void PostStop()
        {
            _helloTask.Cancel();
        }
    }

    static void Main(string[] args)
    {
        using (var system = ActorSystem.Create("Deployer", ConfigurationFactory.ParseString(@"
            akka {  
                actor{
                    provider = remote
                    deployment {
                        /remoteecho {
                            remote = ""akka.tcp://DeployTarget@localhost:8090""
                        }
                    }
                }
                remote {
                    dot-netty.tcp {
                        port = 0
                        hostname = localhost
                    }
                }
            }")))
        {
            var remoteAddress = Address.Parse("akka.tcp://DeployTarget@localhost:8090");
            //deploy remotely via config
            var remoteEcho1 = system.ActorOf(Props.Create(() => new EchoActor()), "remoteecho");

            //deploy remotely via code
            var remoteEcho2 =
                system.ActorOf(
                    Props.Create(() => new EchoActor())
                        .WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))), "coderemoteecho"); 


            system.ActorOf(Props.Create(() => new HelloActor(remoteEcho1)));
            system.ActorOf(Props.Create(() => new HelloActor(remoteEcho2)));

            Console.ReadKey();
        }
    }
}
}