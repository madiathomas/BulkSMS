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
        private readonly IAppSettings _appSettings;

        public AccountProfile(IProfile profile, IAppSettings appSettings)
        {
            _profile = profile;
            _appSettings = appSettings;

            // Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
            _profile.Username = _appSettings.GetSetting("BulkSMSUsername");
            _profile.Password = _appSettings.GetSetting("BulkSMSPassword");
        }

        public async Task<SMSProfile> GetProfile()
        {
            return await _profile.GetProfile();
        }
    }
}
