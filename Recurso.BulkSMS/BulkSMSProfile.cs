using Newtonsoft.Json;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public class BulkSMSProfile : IProfile
    {
        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Construtor . Accepts Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public BulkSMSProfile(string username, string password)
        {
            Username = username;
            Password = password;
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
