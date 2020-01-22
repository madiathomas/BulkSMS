using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS.Common
{

    public class SMSResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }

        [JsonProperty("from")] 
        public string From { get; set; }

        [JsonProperty("to")] 
        public string To { get; set; }

        [JsonProperty("body")] 
        public object Body { get; set; }

        [JsonProperty("encoding")] 
        public string Encoding { get; set; }

        [JsonProperty("protocolId")] 
        public int ProtocolId { get; set; }

        [JsonProperty("messageClass")] 
        public int MessageClass { get; set; }

        [JsonProperty("numberOfParts")] 
        public int NumberOfParts { get; set; }

        [JsonProperty("creditCost")] 
        public int CreditCost { get; set; }

        [JsonProperty("submission")] 
        public Submission Submission { get; set; }

        [JsonProperty("status")] 
        public Status Status { get; set; }

        [JsonProperty("relatedSentMessageId")] 
        public string RelatedSentMessageId { get; set; }

        [JsonProperty("userSuppliedId")] 
        public string UserSuppliedId { get; set; }
    }
}
