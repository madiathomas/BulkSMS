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
        private Mock<IRestClient> restClientMock = new Mock<IRestClient>();
        private Mock<IRestRequest> restRequestMock = new Mock<IRestRequest>();
        private Mock<IRestResponse<SMSResponse>> restResponseMock = new Mock<IRestResponse<SMSResponse>>();

        private BulkSMSProfile bulkSMSProfile;

        [TestInitialize]
        public void Setup()
        {
            SMSProfile smsProfile = new SMSProfile
            {
                Id = "923467170000",
                Username = "madiathomas",
                DateCreated = DateTime.Now,
                Commerce = new Commerce
                {
                    BankPaymentReference = "BankPaymentReference",
                },
                Company = new Company
                {
                    Name = "Company Name",
                    TaxReference = "TaxReference"
                },
                OriginAddresses = new OriginAddresses
                {
                    Allowed = new System.Collections.Generic.List<object>
                    {
                        "Origin Address"
                    }
                },
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

            string json = JsonConvert.SerializeObject(smsProfile);

            restResponseMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);
            restResponseMock.Setup(_ => _.Content).Returns(json);
            restClientMock.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(restResponseMock.Object);

            bulkSMSProfile = new BulkSMSProfile(restClientMock.Object, restRequestMock.Object)
            {
                Username = "Username",
                Password = "Password"
            };
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
        [ExpectedException(typeof(MissingFieldException))]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Username()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSProfile.Username = null;
            bulkSMSProfile.Password = "Password";

            // Act
            await bulkSMSProfile.GetProfile();

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Password()
        {
            // Arrange
            restResponseMock.Setup(_ => _.IsSuccessful).Returns(true);

            bulkSMSProfile.Username = "Username";
            bulkSMSProfile.Password = null;

            // Act
            await bulkSMSProfile.GetProfile();

            // Assert
        }
    }
}
