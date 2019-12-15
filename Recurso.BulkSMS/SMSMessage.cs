using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECMS.Common
{
    public class SMSMessage
    {
        public string to { get; set; }

        public string body { get; set; }

        public int longMessageMaxParts { get; set; }

        public string deliveryReports { get; set; }
    }
}
