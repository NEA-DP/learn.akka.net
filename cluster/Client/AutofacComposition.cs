using Autofac;


namespace Client
{
    public static class AutofacComposition
    {
        public static IContainer Load()
        {
            var builder = new ContainerBuilder();
            
            
            return builder.Build();
        }
    }
}