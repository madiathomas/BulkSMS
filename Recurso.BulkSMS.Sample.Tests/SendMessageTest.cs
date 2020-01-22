using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using Recurso.BulkSMS.Sample.DAL;
using System;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Tests
{
    [TestClass]
    public class SendMessageTest
    {
        readonly Mock<ITextMessage> textMessageMock = new Mock<ITextMessage>();
        readonly Mock<SMSResponse> smsResponseMock = new Mock<SMSResponse>();

        private string phoneNumber = "01234567890";
        private string message = "This is a test message";

        public void Setup()
        {
            
        }

        [TestMethod]
        public async Task SendMessage_Send_Success()
        {
            // Arrange
            textMessageMock.Setup(_ => _.SendSMS(phoneNumber, message)).ReturnsAsync(smsResponseMock.Object);

            var sendMessage = new SendMessage(textMessageMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(phoneNumber, message);

            // Assert

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task SendMessage_Send_Failed()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(phoneNumber, message);

            // Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public async Task SendMessage_Send_Missing_PhoneNumber()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(null, message);

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public async Task SendMessage_Send_Missing_Message()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(phoneNumber, null);

            // Assert
            Assert.IsNotNull(response);
        }
    }
}
