using Autofac;
using Modules.HoconLoader;
using Modules.HoconReader;
using SharedActor;

namespace ClientApp
{
    public class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<HoconReaderModule>();
            builder.RegisterModule<HoconLoaderModule>();
            
            //builder.RegisterType<EchoActor>();
            
            builder.RegisterType<LocalActor>();
            builder.RegisterType<HelloActor>();
            
            return builder.Build();
        }
    }
}