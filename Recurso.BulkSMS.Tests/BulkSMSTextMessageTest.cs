using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Tests
{
    [TestClass]
    public class BulkSMSTextMessageTest
    {
        private readonly string phoneNumber = "+27731234567";
        private readonly string message = "This is a test SMS message";

        private BulkSMSTextMessage bulkSMSTextMessage;

        [TestInitialize]
        public void Setup()
        {
            bulkSMSTextMessage = new BulkSMSTextMessage("Username", "Password");
        }

        // Pseudocode / Plan:
        // - Replace usage of Assert.ThrowsExceptionAsync (not available) with explicit try/catch.
        // - In each test: await the SendSMS call inside try.
        // - If no exception is thrown, call Assert.Fail with a message.
        // - Catch MissingFieldException and assert the caught exception is of the expected type.

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Username()
        {
            // Arrange
            bulkSMSTextMessage.Username = null;
            bulkSMSTextMessage.Password = "Password";

            // Act & Assert
            try
            {
                await bulkSMSTextMessage.SendSMS(phoneNumber, message);
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (MissingFieldException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Password()
        {
            // Arrange
            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = null;

            // Act & Assert
            try
            {
                await bulkSMSTextMessage.SendSMS(phoneNumber, message);
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (MissingFieldException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_PhoneNumber()
        {
            // Arrange
            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = "Password";

            // Act & Assert
            try
            {
                await bulkSMSTextMessage.SendSMS(null, message);
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (MissingFieldException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Message()
        {
            // Arrange
            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = "Password";

            // Act & Assert
            try
            {
                await bulkSMSTextMessage.SendSMS(phoneNumber, null);
                Assert.Fail("Expected MissingFieldException was not thrown.");
            }
            catch (MissingFieldException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(MissingFieldException));
            }
        }
    }
}