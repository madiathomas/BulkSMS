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
    public class BulkSMSProfileTest
    {
        private SMSProfile smsProfile;

        [TestInitialize]
        public void Setup()
        {
            smsProfile = new SMSProfile
            {
                Id = "923467170000",
                Username = "madiathomas",
                DateCreated = DateTime.Now,
                Credits = new Credits
                {
                    Balance = 1000,
                    Limit = 100,
                    IsTransferAllowed = true
                },
                Quota = new Quota
                {
                    Size = 100,
                    Remaining = 10
                }
            };
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(smsProfile);

            var restClientMock = new Mock<IRestClient>();
            var restRequestMock = new Mock<IRestRequest>();
            var restResponseMock = new Mock<IRestResponse<SMSProfile>>();

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            var bulkSMSProfile = new BulkSMSProfile(restClientMock.Object, restRequestMock.Object);

            // Act
            var result = await bulkSMSProfile.GetProfile();

            // Assert
            string actual = "923467170000";
            Assert.AreEqual(result.Id, actual);
        }
    }
}
