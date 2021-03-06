﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS
{
    public class SMSMessage
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("body")] 
        public string Body { get; set; }

        [JsonProperty("longMessageMaxParts")] 
        public int LongMessageMaxParts { get; set; }

        [JsonProperty("deliveryReports")] 
        public string DeliveryReports { get; set; }
    }
}
