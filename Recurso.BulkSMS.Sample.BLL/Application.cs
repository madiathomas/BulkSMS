using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.BLL
{
    public class Application : IApplication
    {
        IBusinessLogic _businessLogic;

        public Application(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        public async Task Run()
        {
            var profile = await _businessLogic.GetProfile();

            Console.WriteLine($"Credit balance: {profile.Credits.Balance}");

            // Send SMS
            string cellNumber = "[Enter Cell Number]"; // Phone number must be in international format e.g +2755520202020
            string message = "This is a test message which was sent via Bulk SMS.";

            SMSResponse response = await _businessLogic.Send(cellNumber, message);

            Console.WriteLine($"Send Message Status: {response.Status}");
        }
    }
}
