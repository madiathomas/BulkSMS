using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Recurso.BulkSMS;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Tests
{
    [TestClass]
    public class BulkSMSProfileTest
    {
        private readonly Mock<IRestClient> restClientMock = new Mock<IRestClient>();
        private readonly Mock<IRestResponse<SMSResponse>> restResponseMock = new Mock<IRestResponse<SMSResponse>>();

        private BulkSMSProfile bulkSMSProfile;

        [TestInitialize]
        public void Setup()
        {
            SMSProfile smsProfile = TestHelpers.GetSMSProfile();

            string json = JsonConvert.SerializeObject(smsProfile);

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            bulkSMSProfile = new BulkSMSProfile("Username", "Password");
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Successful()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            // Act
            var result = await bulkSMSProfile.GetProfile();

            // Assert
            string actual = "923467170000";
            Assert.AreEqual(result.Id, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileNotFoundException))]
        public async Task BulkSMSProfile_GetProfile_Failed()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(false);

            // Act
            var result = await bulkSMSProfile.GetProfile();
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Username()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSProfile.Username = null;
            bulkSMSProfile.Password = "Password";

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSProfile.GetProfile());

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Password()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSProfile.Username = "Username";
            bulkSMSProfile.Password = null;

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSProfile.GetProfile());

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}
