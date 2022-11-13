using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public class BulkSMSTextMessage : ITextMessage
    {
        /// <summary>
        /// Your Bulk SMS user name. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Your Bulk SMS password. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Maximum number of concatenated SMSes when text is more than 140 chracters. Defaulted to 5 messages or 700 characters
        /// </summary>
        public int LongMessageMaximumParts { get; set; } = 5;

        /// <summary>
        /// Construtor
        /// </summary>
        public BulkSMSTextMessage(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Used to Send SMS using Bulk SMS. Phone number must be in an international format.
        /// </summary>
        /// <param name="phoneNumber">Phone number must be in international format</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendSMS(string phoneNumber, string message)
        {
            Username.CheckIfFieldIsMissing();
            Password.CheckIfFieldIsMissing();
            phoneNumber.CheckIfFieldIsMissing();
            message.CheckIfFieldIsMissing();

            var restClient = new RestClient
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password)
            };

            var restRequest = new RestRequest
            {
                Resource = "https://api.bulksms.com/v1/messages",
                Method = Method.Post,
                RequestFormat = DataFormat.Json
            };

            var jsonBody = JsonConvert.SerializeObject(new SMSMessage
            {
                To = phoneNumber,
                Body = message,
                DeliveryReports = "NONE",
                LongMessageMaxParts = LongMessageMaximumParts
            });

            restRequest.AddJsonBody(jsonBody);

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful == false)
            {
                if (response.ErrorException == null)
                {
                    throw new SMSSendFailedException();
                }
                else
                {
                    throw new SMSSendFailedException(response.ErrorException);
                }
            }
        }
    }
}
