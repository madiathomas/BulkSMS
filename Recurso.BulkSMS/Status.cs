using Newtonsoft.Json;

namespace ECMS.Common
{
    public class Status
    {
        [JsonProperty("id")]
        public string StatusId { get; set; }

        [JsonProperty("type")] 
        public string StatusType { get; set; }

        [JsonProperty("subtype")] 
        public string StatusSubtype { get; set; }
    }
}
