using Autofac;
using HoconConfigLoaderModule;
using SharedActors;

namespace NodeSeed
{
    public static class AutofacComposition
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<HoconLoaderModule>();

            builder.RegisterType<ClusterListener>();
            
            builder.RegisterType<DeviceEventsReceiverActor>();
            
            return builder.Build();
        }
    }
}