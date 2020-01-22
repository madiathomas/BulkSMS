using Newtonsoft.Json;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
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

        private readonly IRestClient _restClient;
        private readonly IRestRequest _restRequest;

        /// <summary>
        /// Construtor
        /// </summary>
        public BulkSMSTextMessage(IRestClient restClient, IRestRequest restRequest)
        {
            _restClient = restClient;
            _restRequest = restRequest;
        }

        /// <summary>
        /// Used to Send SMS using Bulk SMS. Phone number must be in an international format.
        /// </summary>
        /// <param name="phoneNumber">Phone number must be in international format</param>
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

            _restClient.Authenticator = new HttpBasicAuthenticator(Username, Password);

            _restRequest.Resource = "https://api.bulksms.com/v1/messages";
            _restRequest.Method = Method.POST;
            _restRequest.RequestFormat = DataFormat.Json;

            var jsonBody = JsonConvert.SerializeObject(new SMSMessage
            {
                To = phoneNumber,
                Body = message,
                DeliveryReports = "NONE",
                LongMessageMaxParts = LongMessageMaximumParts
            });

            _restRequest.AddJsonBody(jsonBody);

            IRestResponse response = await _restClient.ExecuteTaskAsync(_restRequest);

            if (response.IsSuccessful == false)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<SMSResponse>(response.Content);
        }
    }
}
