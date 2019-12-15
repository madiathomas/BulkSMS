using System;
using System.Collections.Generic;
using System.Text;

namespace ECMS.Common
{
    public class Credits
    {
        public double balance { get; set; }
        public int limit { get; set; }
        public bool isTransferAllowed { get; set; }
    }

    public class Quota
    {
        public int size { get; set; }
        public int remaining { get; set; }
    }

    public class OriginAddresses
    {
        public bool isFullControlAllowed { get; set; }
        public List<object> allowed { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string taxReference { get; set; }
    }

    public class Address
    {
    }

    public class Commerce
    {
        public string bankPaymentReference { get; set; }
        public Address address { get; set; }
    }

    public class SMSProfile
    {
        public string id { get; set; }
        public string username { get; set; }
        public DateTime created { get; set; }
        public Credits credits { get; set; }
        public Quota quota { get; set; }
        public OriginAddresses originAddresses { get; set; }
        public Company company { get; set; }
        public Commerce commerce { get; set; }
    }
}
