using Autofac;
using Core;

namespace HoconConfigLoaderModule
{
    public class HoconLoaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HoconLoader>().As<IHoconConfigLoader>();
            
            base.Load(builder);
        }   
    }
}