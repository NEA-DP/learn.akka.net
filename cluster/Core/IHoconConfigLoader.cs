using Akka.Configuration;

namespace Core
{
    public interface IHoconConfigLoader
    {
        Config ParseConfig(string hoconFilePath);
    }
}