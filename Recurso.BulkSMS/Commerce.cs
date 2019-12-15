using Newtonsoft.Json;

namespace ECMS.Common
{
    public class Commerce
    {
        [JsonProperty("bankPaymentReference")]
        public string BankPaymentReference { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}
