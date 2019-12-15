using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace Recurso.BulkSMS
{
    public class BulkSMSer
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int LongMessageMaximumParts { get; set; } = 5;

        /// <summary>
        /// Construtor . Accepts Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public BulkSMSer(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Used to Send SMS using Bulk SMS. Phone number must be in an international format.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<SMSResponse> SendSMS(string phoneNumber, string message)
        {
            // Don't send SMS if cell number or message is null, empty or whitespace
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new FormatException("Message is required!");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new FormatException("Phone number is required!");
            }

            string url = "https://api.bulksms.com/v1/messages";

            var restClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password)
            };

            var restRequest = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var jsonBody = JsonConvert.SerializeObject(new SMSMessage
            {
                To = phoneNumber,
                Body = message,
                DeliveryReports = "ERRORS",
                LongMessageMaxParts = LongMessageMaximumParts
            });

            restRequest.AddJsonBody(jsonBody);

            IRestResponse response = await restClient.ExecuteTaskAsync(restRequest);

            if (response.IsSuccessful == false)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<SMSResponse>(response.Content);
        }

        /// <summary>
        /// Used to get profile information about your bulk sms account. You can use it to retrieve quotas and credits left.
        /// </summary>
        /// <returns></returns>
        public async Task<SMSProfile> GetProfile()
        {
            string url = "https://api.bulksms.com/v1/profile";

            var restClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password)
            };

            var restRequest = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            IRestResponse response = await restClient.ExecuteTaskAsync(restRequest);

            if (response.IsSuccessful == false)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<SMSProfile>(response.Content);
        }
    }
}
