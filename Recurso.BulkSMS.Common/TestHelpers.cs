using Recurso.BulkSMS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS.Common
{
    public class TestHelpers
    {
        public static SMSProfile GetSMSProfile()
        {
            return new SMSProfile
            {
                Id = "923467170000",
                Username = "Username",
                DateCreated = DateTime.Now,
                Commerce = new Commerce
                {
                    BankPaymentReference = "BankPaymentReference",
                },
                Company = new Company
                {
                    Name = "Company Name",
                    TaxReference = "TaxReference"
                },
                OriginAddresses = new OriginAddresses
                {
                    Allowed = new System.Collections.Generic.List<object>
                    {
                        "Origin Address"
                    }
                },
                Credits = new Credits
                {
                    Balance = 1000,
                    Limit = 100,
                    IsTransferAllowed = true
                },
                Quota = new Quota
                {
                    Size = 100,
                    Remaining = 10
                }
            };
        }

        public static SMSResponse GetSMSResponse(string phoneNumber, string message)
        {
            return new SMSResponse
            {
                To = phoneNumber,
                Body = message,
                CreditCost = 1,
                Status = new Status
                {
                    StatusId = "0",
                    StatusType = "ACCEPTED",
                    StatusSubtype = "SENT"
                },
                Submission = new Submission
                {
                    SubmissionDate = DateTime.Now,
                    SubmissionId = "SubmissionId"
                }
            };
        }

    }
}
