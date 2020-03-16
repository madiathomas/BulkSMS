using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public interface IProfile
    {
        string Password { get; set; }

        string Username { get; set; }

        Task<SMSProfile> GetProfile();
    }
}
