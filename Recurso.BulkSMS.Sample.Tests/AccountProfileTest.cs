using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recurso.BulkSMS.Common;
using Recurso.BulkSMS.Common.Interfaces;
using Recurso.BulkSMS.Sample.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recurso.BulkSMS.Sample.Tests
{
    [TestClass]
    public class AccountProfileTest
    {
        readonly Mock<SMSProfile> smsProfileMock = new Mock<SMSProfile>();
        readonly Mock<IProfile> profileMock = new Mock<IProfile>();

        private SMSProfile smsProfile;
        [TestInitialize]
        public void Setup() {
            smsProfile = new SMSProfile
            {
                Id = "923467170000",
                Username = "Username",
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
        }

        [TestMethod]
        public async Task AccountProfile_GetProfile_Success()
        {
            // Arrange
            profileMock.Setup(_ => _.GetProfile()).ReturnsAsync(smsProfile);

            var accountProfile = new AccountProfile(profileMock.Object);

            // Act
            SMSProfile profile = await accountProfile.GetProfile();

            // Assert
            Assert.IsNotNull(profile);
        }

        [TestMethod]
        public async Task AccountProfile_GetProfile_Failed()
        {
            // Arrange
            var accountProfile = new AccountProfile(profileMock.Object);

            // Act
            SMSProfile profile = await accountProfile.GetProfile();

            // Assert
            Assert.IsNull(profile);
        }
    }
}
