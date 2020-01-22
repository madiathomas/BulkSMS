using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS.Sample.DAL
{
    public class AppSettings
    {
        private static readonly IConfiguration configuration = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                               .AddJsonFile("appsettings.json")
                                               .Build();

        public static string Username = configuration["AppSettings:BulkSMSUsername"];
        public static string Password = configuration["AppSettings:BulkSMSPassword"];
    }
}
