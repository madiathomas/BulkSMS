
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        readonly IAccountProfile _accountProfile;
        readonly ISendMessage _sendMessage;

        public BusinessLogic(IAccountProfile accountProfile, ISendMessage sendMessage)
        {
            _accountProfile = accountProfile;
            _sendMessage = sendMessage;
        }

        public async Task<SMSProfile> GetProfile()
        {
            return await _accountProfile.GetProfile();
        }

        public async Task<SMSResponse> Send(string phoneNumber, string message)
        {
            return await _sendMessage.Send(phoneNumber, message);
        }
    }
}
