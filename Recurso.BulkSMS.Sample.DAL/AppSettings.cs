using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS.Sample.DAL
{
    public class AppSettings
    {
        public static string Username { get; set; }
        public static string Password { get; set; }

        public AppSettings()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();

            Username = configuration["AppSettings:BulkSMSUsername"];
            Password = configuration["AppSettings:BulkSMSPassword"];
        }
    }
}
