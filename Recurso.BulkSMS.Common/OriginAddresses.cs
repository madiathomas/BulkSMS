using Newtonsoft.Json;
using System.Collections.Generic;

namespace Recurso.BulkSMS.Common
{
    public class OriginAddresses
    {
        [JsonProperty("isFullControlAllowed")]
        public bool IsFullControlAllowed { get; set; }

        [JsonProperty("allowed")]
        public List<object> Allowed { get; set; }
    }
}
