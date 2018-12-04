using System.IO;
using Core;

namespace Modules.HoconReader
{
    public class HoconReader : IHoconFileReader
    {
        public string RadAllText(string hoconFilePath)
        {
            return File.ReadAllText(hoconFilePath);
        }
    }
}