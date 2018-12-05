using Autofac;
using HoconConfigLoaderModule;
using SharedActors;

namespace NodeSome
{
    public static class AutofacComposition
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<HoconLoaderModule>();

            builder.RegisterType<ClusterListener>();
            
            return builder.Build();
        }
    }
}