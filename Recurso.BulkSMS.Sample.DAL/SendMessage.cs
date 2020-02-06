using Recurso.BulkSMS;

using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.DAL
{
    public class SendMessage : ISendMessage
    {
        readonly ITextMessage _textMessage;
        readonly IAppSettings _appSettings;

        public SendMessage(ITextMessage textMessage, IAppSettings appSettings)
        {
            _textMessage = textMessage;
            _appSettings = appSettings;

            // Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
            _textMessage.Username = _appSettings.GetSetting("BulkSMSUsername");
            _textMessage.Password = _appSettings.GetSetting("BulkSMSPassword");
        }

        public async Task<SMSResponse> Send(string phoneNumber, string message)
        {
            phoneNumber.CheckIfFieldIsMissing();
            message.CheckIfFieldIsMissing();

            return await _textMessage.SendSMS(phoneNumber, message);
        }
    }
}
