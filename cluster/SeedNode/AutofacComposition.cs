using Autofac;
using SharedActors;

namespace SeedNode
{
    public static class AutofacComposition
    {
        public static IContainer Load()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ClusterListener>();
            
            return builder.Build();
        }
    }
}