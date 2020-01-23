using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Sample.BLL;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using Recurso.BulkSMS.Sample.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Tests
{
    [TestClass]
    public class BusinessLogicTest
    {
        Mock<IAccountProfile> accountProfileMock = new Mock<IAccountProfile>();
        Mock<ISendMessage> sendMessageMock = new Mock<ISendMessage>();

        private readonly string phoneNumber = "+27731234567";
        private readonly string message = "This is a test SMS message";

        private SMSProfile smsProfile;
        private SMSResponse smsResponse;

        [TestInitialize]
        public void Setup()
        {
            smsProfile = TestHelpers.GetSMSProfile();
            smsResponse = TestHelpers.GetSMSResponse(phoneNumber, message);
        }

        [TestMethod]
        public async Task BusinessLogic_GetProfile_Success()
        {
            // Arrange
            accountProfileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSProfile result = await businessLogic.GetProfile();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task BusinessLogic_Send_Success()
        {
            // Arrange
            sendMessageMock.Setup(_ => _.Send(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(smsResponse);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSResponse result = await businessLogic.Send(phoneNumber, message);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task BusinessLogic_Send_Missing_PhoneNumber()
        {
            // Arrange
            sendMessageMock.Setup(_ => _.Send(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(smsResponse);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async ()=> await businessLogic.Send(null, message));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task BusinessLogic_Send_Missing_Message()
        {
            // Arrange
            sendMessageMock.Setup(_ => _.Send(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(smsResponse);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            var result = await  Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await businessLogic.Send(phoneNumber, null));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}
