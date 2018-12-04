using Autofac;
using SharedActors;
using SomeNode.Actors;


namespace SomeNode
{
    public static class AutofacComposition
    {
        public static IContainer Load()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ClusterListener>();
            builder.RegisterType<EchoResponserActor>();
            
            return builder.Build();
        }
    }
}