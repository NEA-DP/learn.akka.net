using Autofac;

namespace HostApp
{
    public static class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleMessageService>().As<IMessageService>();
            builder.RegisterType<MessageActor>();
            builder.RegisterType<MessageActorInitializerActor>();
            
            return builder.Build();
        }
    }
}