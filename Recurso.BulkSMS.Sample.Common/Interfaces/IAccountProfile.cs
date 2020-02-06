using Recurso.BulkSMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Common.Interfaces
{
    public interface IAccountProfile
    {
        Task<SMSProfile> GetProfile();
    }
}
