using System;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample
{
    class Program
    {
        static async Task Main()
        {
            string username = "[Your Bulk SMS Username]";
            string password = "[Your Bulk SMS Password]";

            // Get Profile details
            var bulkSMSProfile = new BulkSMSProfile(username, password);
            var profile = await bulkSMSProfile.GetProfile();
            Console.WriteLine($"Credit balance: {profile.Credits.Balance}");

            // Send SMS
            string cellNumber = "[Enter Cell Number]"; // Phone number must be in international format e.g +2755520202020
            string message = "This is a test message which was sent via Bulk SMS.";

            var bulkSMSTextMessage = new BulkSMSTextMessage(username, password);
            SMSResponse response = await bulkSMSTextMessage.SendSMS(cellNumber, message);

            Console.WriteLine($"Send Message Status: {response.Status}");
        }
    }
}
