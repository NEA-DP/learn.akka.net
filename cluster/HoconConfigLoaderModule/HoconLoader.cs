using System.IO;
using Akka.Configuration;
using Core;

namespace HoconConfigLoaderModule
{
    public class HoconLoader : IHoconConfigLoader
    {
        public Config ParseConfig(string hoconFilePath)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(hoconFilePath));
        }
    }
}