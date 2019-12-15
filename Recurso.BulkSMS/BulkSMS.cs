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

namespace ECMS.Common
{
    public class BulkSMS
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int LongMessageMaximumParts { get; set; } = 5;

        public BulkSMS(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public async Task<SMSResponse> SendSMS(string message, string cellnumber)
        {
            // Don't send SMS if cell number or message is null, empty or whitespace
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(cellnumber))
            {
                throw new FormatException();
            }

            string msisdn = cellnumber;

            string url = "https://api.bulksms.com/v1/messages";

            var restClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password)
            };

            var restRequest = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var body = new SMSMessage
            {
                To = msisdn,
                Body = message,
                DeliveryReports = "ERRORS",
                LongMessageMaxParts = LongMessageMaximumParts
            };

            restRequest.AddJsonBody(body);

            IRestResponse response = await restClient.ExecuteTaskAsync(restRequest);

            if (response.IsSuccessful == false)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<SMSResponse>(response.Content);
        }

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
