using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            bulkSMSProfile = new BulkSMSProfile("Username", "Password");
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Failed()
        {
            try
            {
                await bulkSMSProfile.GetProfile();
                Assert.Fail("Expected ProfileNotFoundException was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ProfileNotFoundException));
            }
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Username()
        {
            // Arrange
            bulkSMSProfile.Username = null;
            bulkSMSProfile.Password = "Password";

            // Act & Assert
            try
            {
                await bulkSMSProfile.GetProfile();
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }

        [TestMethod]
        public async Task BulkSMSProfile_GetProfile_Test_Missing_Password()
        {
            // Arrange
            bulkSMSProfile.Username = "Username";
            bulkSMSProfile.Password = null;

            // Act & Assert
            try
            {
                await bulkSMSProfile.GetProfile();
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }
    }
}
