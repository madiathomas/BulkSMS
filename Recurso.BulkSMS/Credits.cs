using Newtonsoft.Json;

namespace ECMS.Common
{
    public class Credits
    {
        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("isTransferAllowed")]
        public bool IsTransferAllowed { get; set; }
    }
}
