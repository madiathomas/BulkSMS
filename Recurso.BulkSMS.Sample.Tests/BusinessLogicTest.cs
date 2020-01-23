using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Sample.BLL;
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
        Mock<AccountProfile> accountProfileMock = new Mock<AccountProfile>();
        Mock<SendMessage> sendMessageMock = new Mock<SendMessage>();

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
        public async Task BusinessLogic_GetProfile_Failed()
        {
            // Arrange
            smsProfile = null;

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
            accountProfileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSResponse result = await businessLogic.Send(phoneNumber, message);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task BusinessLogic_Send_Failed()
        {
            // Arrange
            accountProfileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSResponse result = await businessLogic.Send(phoneNumber, message);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMemberException))]
        public async Task BusinessLogic_Send_Missing_PhoneNumber()
        {
            // Arrange
            accountProfileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSResponse result = await businessLogic.Send(null, message);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMemberException))]
        public async Task BusinessLogic_Send_Missing_Message()
        {
            // Arrange
            accountProfileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var businessLogic = new BusinessLogic(accountProfileMock.Object, sendMessageMock.Object);

            // Act
            SMSResponse result = await businessLogic.Send(phoneNumber, null);

            // Assert
        }
    }
}
