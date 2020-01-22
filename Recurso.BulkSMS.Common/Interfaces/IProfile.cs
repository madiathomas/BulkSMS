using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Common.Interfaces
{
    public interface IProfile
    {
        string Password { get; set; }
        string Username { get; set; }

        Task<SMSProfile> GetProfile();
    }
}
