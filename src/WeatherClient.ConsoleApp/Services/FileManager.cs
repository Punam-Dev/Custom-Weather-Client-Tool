using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeatherClient.ConsoleApp.Interfaces;

namespace WeatherClient.ConsoleApp.Services
{
    public class FileManager :IFileManager
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
