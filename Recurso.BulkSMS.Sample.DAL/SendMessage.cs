using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
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

        public SendMessage(ITextMessage textMessage)
        {
            _textMessage = textMessage;

            // Bulk SMS username and password. To sign up and get free test credits, go to https://www.bulksms.com.
            _textMessage.Username = AppSettings.Username;
            _textMessage.Password = AppSettings.Password;
        }

        public async Task<SMSResponse> Send(string phoneNumber, string message)
        {
            return await _textMessage.SendSMS(phoneNumber, message);
        }
    }
}
