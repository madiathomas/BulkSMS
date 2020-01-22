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
            var businessLogic = container.Resolve<IBusinessLogic>();

            var profile = await businessLogic.GetProfile();

            Console.WriteLine($"Credit balance: {profile.Credits.Balance}");

            // Send SMS
            string cellNumber = "[Enter Cell Number]"; // Phone number must be in international format e.g +2755520202020
            string message = "This is a test message which was sent via Bulk SMS.";

            SMSResponse response = await businessLogic.Send(cellNumber, message);

            Console.WriteLine($"Send Message Status: {response.Status}");
        }
    }
}
