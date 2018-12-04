using Autofac;
using Core;

namespace Modules.HoconLoader
{
    public class HoconLoaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HoconLoader>().As<IHoconFileLoader>();
            
            base.Load(builder);
        }   
    }
}