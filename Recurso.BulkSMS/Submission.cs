﻿using Newtonsoft.Json;
using System;

namespace ECMS.Common
{
    public class Submission
    {
        [JsonProperty("id")]
        public string SubmissionId { get; set; }

        [JsonProperty("date")]
        public DateTime SubmissionDate { get; set; }
    }
}
