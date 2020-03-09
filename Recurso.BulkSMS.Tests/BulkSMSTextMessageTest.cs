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

        [TestMethod]
        [ExpectedException(typeof(SMSSendFailedException))]
        public async Task BulkSMSTextMessage_SendSMS_Failed()
        {
            var result = await bulkSMSTextMessage.SendSMS(phoneNumber, message);

            string actual = "ACCEPTED";
            Assert.AreEqual(result.Status.StatusType, actual);
        }

        [TestMethod]
        public async Task BulkSMSTextMessage_SendSMS_Test_Missing_Username()
        {
            // Arrange
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
            bulkSMSTextMessage.Username = "Username";
            bulkSMSTextMessage.Password = "Password";

            // Act
            var result = await Assert.ThrowsExceptionAsync<MissingFieldException>(async () => await bulkSMSTextMessage.SendSMS(phoneNumber, null));

            // Assert
            Assert.IsInstanceOfType(result, typeof(MissingFieldException));
        }
    }
}