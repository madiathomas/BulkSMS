using Newtonsoft.Json;
using System;

namespace Recurso.BulkSMS
{
    public class Submission
    {
        [JsonProperty("id")]
        public string SubmissionId { get; set; }

        [JsonProperty("date")]
        public DateTime SubmissionDate { get; set; }
    }
}
