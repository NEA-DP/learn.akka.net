using Autofac;

namespace ClientApp
{
    public class AutofacConfig
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LocalActor>();
            
            return builder.Build();
        }
    }
}