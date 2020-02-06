using Newtonsoft.Json;
using System;
using System.Text;

namespace Recurso.BulkSMS
{
    public class SMSProfile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }

        [JsonProperty("quota")]
        public Quota Quota { get; set; }

        [JsonProperty("originAddresses")]
        public OriginAddresses OriginAddresses { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("commerce")]
        public Commerce Commerce { get; set; }
    }
}
