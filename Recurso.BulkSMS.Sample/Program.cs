using System;
using System.Configuration;
using System.Threading.Tasks;
using Autofac;
using Recurso.BulkSMS;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Sample.BLL;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using RestSharp;

namespace Recurso.BulkSMS.Sample
{
    class Program
    {
        static async Task Main()
        {
            var container = ContainerConfiguration.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                await app.Run();
            }
        }
    }
}
