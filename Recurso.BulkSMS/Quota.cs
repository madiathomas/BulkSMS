using Newtonsoft.Json;

namespace ECMS.Common
{
    public class Quota
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("remaining")]
        public int Remaining { get; set; }
    }
}
