using System;
using System.Configuration;
using System.Threading.Tasks;
using Recurso.BulkSMS;

namespace Recurso.BulkSMS.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
            string username = "[Your Bulk SMS Username]";
            string password = "[Your Bulk SMS Password]";

            var bulkSMS = new BulkSMSer(username, password);

            // Get profile
            var profile = await bulkSMS.GetProfile();
            Console.WriteLine($"Credit balance: {profile.Credits.Balance}");

            // Send SMS
            string cellNumber = "[Enter your number";
            string message = "This is a test message which was sent via Bulk SMS.";
            await bulkSMS.SendSMS(cellNumber, message);
        }
    }
}
