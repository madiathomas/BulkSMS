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

        [TestInitialize]
        public void Setup()
        {
            smsResponse = new SMSResponse
            {
                To = phoneNumber,
                Body = message,
                CreditCost = 1,
                Status = new Status
                {
                    StatusId = "0",
                    StatusType = "ACCEPTED",
                    StatusSubtype = "SENT"
                }
            };
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Successful()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(smsResponse);

            var restClientMock = new Mock<IRestClient>();
            var restRequestMock = new Mock<IRestRequest>();
            var restResponseMock = new Mock<IRestResponse<SMSResponse>>();

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            var bulkSMSProfile = new BulkSMSTextMessage(restClientMock.Object, restRequestMock.Object);

            // Act
            var result = await bulkSMSProfile.SendSMS(phoneNumber, message);

            // Assert
            string actual = "ACCEPTED";
            Assert.AreEqual(result.Status.StatusType, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SMSSendFailedException))]
        public async Task BulkSMSTextMessage_SendSMS_Failed()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(smsResponse);

            var restClientMock = new Mock<IRestClient>();
            var restRequestMock = new Mock<IRestRequest>();
            var restResponseMock = new Mock<IRestResponse<SMSResponse>>();

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(false);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            var bulkSMSProfile = new BulkSMSTextMessage(restClientMock.Object, restRequestMock.Object);

            // Act
            var result = await bulkSMSProfile.SendSMS(phoneNumber, message);

            // Assert
            string actual = "ACCEPTED";
            Assert.AreEqual(result.Status.StatusType, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_PhoneNumber()
        {
            // Arrange
            var restClientMock = new Mock<IRestClient>();
            var restRequestMock = new Mock<IRestRequest>();
            var restResponseMock = new Mock<IRestResponse<SMSResponse>>();

            restResponseMock.Setup(_ => _.IsSuccessful).Returns(false);

            var bulkSMSProfile = new BulkSMSTextMessage(restClientMock.Object, restRequestMock.Object);

            // Act
            await bulkSMSProfile.SendSMS(null, message);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Message()
        {
            // Arrange
            var restClientMock = new Mock<IRestClient>();
            var restRequestMock = new Mock<IRestRequest>();
            var restResponseMock = new Mock<IRestResponse<SMSResponse>>();

            restResponseMock.Setup(_ => _.IsSuccessful).Returns(false);

            var bulkSMSProfile = new BulkSMSTextMessage(restClientMock.Object, restRequestMock.Object);

            // Act
            await bulkSMSProfile.SendSMS(phoneNumber, null);

            // Assert
        }
    }
}