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
        private BulkSMSProfile bulkSMSProfile;

        [TestInitialize]
        public void Setup()
        {
            SMSProfile smsProfile = TestHelpers.GetSMSProfile();

            string json = JsonConvert.SerializeObject(smsProfile);

            bulkSMSProfile = new BulkSMSProfile("Username", "Password");
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileNotFoundException))]
        public async Task BulkSMSProfile_GetProfile_Failed()
        {
            var result = await bulkSMSProfile.GetProfile();
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Username()
        {
            // Arrange
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
            bulkSMSProfile.Username = "Username";
            bulkSMSProfile.Password = null;

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSProfile.GetProfile());

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}
