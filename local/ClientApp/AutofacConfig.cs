using Autofac;
using Modules.HoconLoader;
using Modules.HoconReader;

namespace ClientApp
{
    public class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<HoconReaderModule>();
            builder.RegisterModule<HoconLoaderModule>();
            
            builder.RegisterType<LocalActor>();
            
            return builder.Build();
        }
    }
}