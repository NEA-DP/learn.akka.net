using Autofac;
using Modules.HoconLoader;
using Modules.HoconReader;

namespace HostApp
{
    public static class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<HoconReaderModule>();
            builder.RegisterModule<HoconLoaderModule>();
            
            builder.RegisterType<ConsoleMessageService>().As<IMessageService>();
            builder.RegisterType<MessageActor>();
            builder.RegisterType<MessageActorInitializerActor>();
            
            return builder.Build();
        }
    }
}