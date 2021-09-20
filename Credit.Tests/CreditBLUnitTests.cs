using System;
using System.Threading.Tasks;
using Credit.BLL;
using Credit.Core.DAL;
using Moq;
using NUnit.Framework;

namespace Credit.Tests
{
    public class CreditBLUnitTests
    {
        [Test]
        public async Task Test_CreateCredit_Pass()
        {
            var mockDR = new Mock<ICreditDataRepository>();
            mockDR.Setup(c => c.CreateCredit(It.IsAny<Core.Models.Credit>()))
                .Returns(() => Task<Core.Models.Credit>.FromResult(new Core.Models.Credit()
                {
                    Id = Guid.Empty, Amount = 123, Type = "TestType", ApplicationId = 101, Summary = "Payment"
                }));
            var creditBL = new CreditBL(mockDR.Object);
            var newCredit = await creditBL.CreateCredit(new Core.Models.Credit());
            Assert.IsNotNull(newCredit);
            Assert.AreEqual(123, newCredit.Amount);
            Assert.AreEqual("Payment", newCredit.Summary);
            Assert.AreEqual(101, newCredit.ApplicationId);
            Assert.AreEqual("TestType", newCredit.Type);
        }
    }
}