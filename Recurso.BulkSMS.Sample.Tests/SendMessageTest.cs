using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using Recurso.BulkSMS.Sample.DAL;
using System;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Tests
{
    [TestClass]
    public class SendMessageTest
    {
        readonly Mock<ITextMessage> textMessageMock = new Mock<ITextMessage>();
        readonly Mock<IAppSettings> appSettingsMock = new Mock<IAppSettings>();

        private SMSResponse smsResponse;

        private readonly string phoneNumber = "01234567890";
        private readonly string message = "This is a test message";

        [TestInitialize]
        public void Setup()
        {
            smsResponse = TestHelpers.GetSMSResponse(phoneNumber, message);
        }

        [TestMethod]
        public async Task SendMessage_Send_Success()
        {
            // Arrange
            textMessageMock.Setup(_ => _.SendSMS(phoneNumber, message)).ReturnsAsync(smsResponse);

            var sendMessage = new SendMessage(textMessageMock.Object, appSettingsMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(phoneNumber, message);

            // Assert

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task SendMessage_Send_Failed()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object, appSettingsMock.Object);

            // Act
            SMSResponse response = await sendMessage.Send(phoneNumber, message);

            // Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task SendMessage_Send_Missing_PhoneNumber()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object, appSettingsMock.Object);

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async ()=> await sendMessage.Send(null, message));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task SendMessage_Send_Missing_Message()
        {
            // Arrange
            var sendMessage = new SendMessage(textMessageMock.Object, appSettingsMock.Object);

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await sendMessage.Send(phoneNumber, null));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}
