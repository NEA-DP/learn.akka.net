using Autofac;
using Core;

namespace Modules.HoconReader
{
    public class HoconReaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HoconReader>().As<IHoconFileReader>();
            
            base.Load(builder);
        }
    }
}