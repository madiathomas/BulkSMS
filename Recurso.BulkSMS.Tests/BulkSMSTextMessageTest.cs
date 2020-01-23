using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Recurso.BulkSMS.Common;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Tests
{
    [TestClass]
    public class BulkSMSTextMessageTest
    {
        private readonly string phoneNumber = "+27731234567";
        private readonly string message = "This is a test SMS message";
        private SMSResponse smsResponse;
        private string json;

        private Mock<IRestClient> restClientMock = new Mock<IRestClient>();
        private Mock<IRestRequest> restRequestMock = new Mock<IRestRequest>();
        private Mock<IRestResponse<SMSResponse>> restResponseMock = new Mock<IRestResponse<SMSResponse>>();
        private BulkSMSTextMessage bulkSMSTextMessage;

        [TestInitialize]
        public void Setup()
        {
            smsResponse = TestHelpers.GetSMSResponse(phoneNumber,message);

            json = JsonConvert.SerializeObject(smsResponse);

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            bulkSMSTextMessage = new BulkSMSTextMessage(restClientMock.Object, restRequestMock.Object)
            {
                Username = "Username",
                Password = "Password"
            };
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Successful()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            // Act
            var result = await bulkSMSTextMessage.SendSMS(phoneNumber, message);

            // Assert
            string actual = "ACCEPTED";
            Assert.AreEqual(result.Status.StatusType, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SMSSendFailedException))]
        public async Task BulkSMSTextMessage_SendSMS_Failed()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(false);

            // Act
            var result = await bulkSMSTextMessage.SendSMS(phoneNumber, message);

            // Assert
            string actual = "ACCEPTED";
            Assert.AreEqual(result.Status.StatusType, actual);
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Username()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSTextMessage.Username = null;
            bulkSMSTextMessage.Password = "Password";

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSTextMessage.SendSMS(phoneNumber, message));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Password()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = null;

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSTextMessage.SendSMS(phoneNumber, message));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_PhoneNumber()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = "Password";

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSTextMessage.SendSMS(null, message));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Message()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = "Password";

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSTextMessage.SendSMS(phoneNumber, null));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}