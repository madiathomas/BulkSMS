using Newtonsoft.Json;

namespace Recurso.BulkSMS.Common
{
    public class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("taxReference")]
        public string TaxReference { get; set; }
    }
}
