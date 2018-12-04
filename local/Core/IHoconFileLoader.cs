using Akka.Configuration;

namespace Core
{
    public interface IHoconFileLoader
    {
        Config ParseConfig(string hoconFilePath);
    }
}