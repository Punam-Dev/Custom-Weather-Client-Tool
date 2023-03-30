using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherClient.ConsoleApp.Interfaces
{
    public interface IFileManager
    {
        StreamReader StreamReader(string path);
    }
}
