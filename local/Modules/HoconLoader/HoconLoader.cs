using Akka.Configuration;
using Core;

namespace Modules.HoconLoader
{
    public class HoconLoader : IHoconFileLoader
    {
        private readonly IHoconFileReader _hoconFileReader;

        public HoconLoader(IHoconFileReader hoconFileReader)
        {
            _hoconFileReader = hoconFileReader;
        }
        
        public Config ParseConfig(string hoconFilePath)
        {
            return ConfigurationFactory.ParseString(_hoconFileReader.RadAllText(hoconFilePath));
        }
    }
}