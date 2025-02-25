using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
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

        /// <summary>
        /// Construtor 
        /// </summary>
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
            Username.CheckIfFieldIsMissing();
            Password.CheckIfFieldIsMissing();

            var restClient = new RestClient(new RestClientOptions
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password)
            });

            var restRequest = new RestRequest
            {
                Resource = "https://api.bulksms.com/v1/profile",
                Method = Method.Get,
                RequestFormat = DataFormat.Json
            };

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessful == false)
            {
                if (response.ErrorException == null)
                {
                    throw new ProfileNotFoundException();
                }
                else
                {
                    throw new ProfileNotFoundException(response.ErrorException);
                }
            }

            return JsonConvert.DeserializeObject<SMSProfile>(response.Content);
        }
    }
}
