using System;
using System.Collections.Generic;
using System.Text;

namespace ECMS.Common
{
    public class Submission
    {
        public string id { get; set; }
        public DateTime date { get; set; }
    }

    public class Status
    {
        public string id { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
    }

    public class SMSResponse
    {
        public string id { get; set; } 

        public string type { get; set; }

        public string from { get; set; }

        public string to { get; set; }

        public object body { get; set; }

        public string encoding { get; set; }

        public int protocolId { get; set; }

        public int messageClass { get; set; }

        public int numberOfParts { get; set; }

        public int creditCost { get; set; }

        public Submission submission { get; set; }

        public Status status { get; set; }

        public string relatedSentMessageId { get; set; }

        public string userSuppliedId { get; set; }
    }
}
