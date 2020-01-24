using Microsoft.Extensions.Configuration;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.IO;

namespace Recurso.BulkSMS.Sample.DAL
{
    public class AppSettings : IAppSettings
    {
        private readonly string directory = System.IO.Directory.GetCurrentDirectory();
        private readonly string jsonFileName = "appsettings.json";

        public string GetSetting(string settingName)
        {
            try
            {
                var configurationRoot = new ConfigurationBuilder().SetBasePath(directory).AddJsonFile(jsonFileName).Build();
                return configurationRoot[$"AppSettings:{settingName}"];
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
    }
}
