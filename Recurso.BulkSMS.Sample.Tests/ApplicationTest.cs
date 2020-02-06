using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS;
using Recurso.BulkSMS.Sample.BLL;
using Recurso.BulkSMS.Sample.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Tests
{
    [TestClass]
    public class ApplicationTest
    {
        readonly Mock<IBusinessLogic> businessLogicMock = new Mock<IBusinessLogic>();

        private readonly string phoneNumber = "01234567890";
        private readonly string message = "This is a test message";

        private SMSProfile smsProfile;
        private SMSResponse smsResponse;

        [TestInitialize]
        public void Setup()
        {
            smsProfile = TestHelpers.GetSMSProfile();
            smsResponse = TestHelpers.GetSMSResponse(phoneNumber, message);
        }

        [TestMethod]
        public void Application_Run()
        {
            // Arrange
            businessLogicMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);
            businessLogicMock.Setup(_ => _.Send(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(smsResponse);

            var application = new Application(businessLogicMock.Object);

            // Act
            Task task = application.Run();

            // Assert
            Assert.IsInstanceOfType(task, typeof(Task));
        }
    }
}
