using Newtonsoft.Json;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public class BulkSMSProfile : IProfile
    {
        /// <summary>
        /// Your Bulk SMS user name. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Your Bulk SMS password. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        public string Password { get; set; }

        private readonly IRestClient _restClient;
        private readonly IRestRequest _restRequest;

        /// <summary>
        /// Construtor 
        /// </summary>
        public BulkSMSProfile(IRestClient restClient, IRestRequest restRequest)
        {
            _restClient = restClient;
            _restRequest = restRequest;
        }

        /// <summary>
        /// Used to get profile information about your bulk sms account. You can use it to retrieve quotas and credits left.
        /// </summary>
        /// <returns></returns>
        public async Task<SMSProfile> GetProfile()
        {
            Username.CheckIfFieldIsMissing();
            Password.CheckIfFieldIsMissing();

            _restClient.Authenticator = new HttpBasicAuthenticator(Username, Password);

            _restRequest.RequestFormat = DataFormat.Json;
            _restRequest.Method = Method.GET;
            _restRequest.Resource = "https://api.bulksms.com/v1/profile";

            IRestResponse response = await _restClient.ExecuteTaskAsync(_restRequest);

            if (response.IsSuccessful == false)
            {
                throw new ProfileNotFoundException(response.ErrorException);
            }

            return JsonConvert.DeserializeObject<SMSProfile>(response.Content);
        }
    }
}
