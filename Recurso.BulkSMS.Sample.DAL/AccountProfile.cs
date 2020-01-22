using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.DAL
{
    public class AccountProfile : IAccountProfile
    {
        readonly IProfile _profile;

        public AccountProfile(IProfile profile)
        {
            _profile = profile;

            // Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
            _profile.Username = AppSettings.Username;
            _profile.Password = AppSettings.Password;
        }

        public async Task<SMSProfile> GetProfile()
        {
            return await _profile.GetProfile();
        }
    }
}
