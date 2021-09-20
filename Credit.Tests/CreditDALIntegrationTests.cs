using System;
using System.Threading.Tasks;
using Credit.Core.DAL;
using Credit.Core.Models;
using NUnit.Framework;
using Credit.MockDAL;

namespace Credit.Tests
{
    public class CreditDALIntegrationTests
    {
        private ICreditDataRepository _dataRepository;
        
        [SetUp]
        public void Setup()
        {
            _dataRepository = new MockCreditDataRepository();
        }
        
        [Test]
        public async Task Test_CreateCredit_Pass()
        {
            var credit = new Core.Models.Credit()
            {
                Amount = 123, Summary = "Payment", Type = "TestType", ApplicationId = 101
            };
            
            var newCredit = await _dataRepository.CreateCredit(credit);
            Assert.IsNotNull(newCredit);
            Assert.AreEqual(123, newCredit.Amount);
            Assert.AreEqual("Payment", newCredit.Summary);
            Assert.AreEqual(101, newCredit.ApplicationId);
            Assert.AreEqual("TestType", newCredit.Type);
            
            await _dataRepository.DeleteCredit(newCredit.Id);
        }

        [Test]
        public async Task Test_UpdateCredit_Pass()
        {
            var guid = Guid.Parse("d2032222-47a6-4048-9894-11ab8ebb9f69");
            var credit = await _dataRepository.GetCreditById(guid);
            credit.Amount = 123;
            credit.Summary = "TestPayment";
            credit.Type = "TestType";
            credit.ApplicationId = 101;

            var updateCredit = await _dataRepository.UpdateCredit(credit);
            
            Assert.IsNotNull(updateCredit);
            Assert.AreEqual(123, updateCredit.Amount);
            Assert.AreEqual("TestPayment", updateCredit.Summary);
            Assert.AreEqual(101, updateCredit.ApplicationId);
            Assert.AreEqual("TestType", updateCredit.Type);
        }

        [Test]
        public async Task Test_DeleteCredit_Pass()
        {
            var guid = Guid.Parse("3f2b12b8-2a06-45b4-b057-45949279b4e5");
            var credit = await _dataRepository.GetCreditById(guid);
            Assert.IsNotNull(credit);
            
            await _dataRepository.DeleteCredit(guid);
            
            credit = await _dataRepository.GetCreditById(guid);
            Assert.IsNull(credit);
        }

        [Test]
        public async Task Test_GetCredits_NotEmpty()
        {
            var credits = await _dataRepository.GetCredits(new CreditFilter());

            Assert.IsNotEmpty(credits);
        }

        [Test]
        public async Task Test_GetCreditById_Pass()
        {
            var guid = Guid.Parse("7a66f608-2979-4b79-ba2e-d9b4d573b294");
            var credit = await _dataRepository.GetCreditById(guid);
            
            Assert.AreEqual(guid, credit.Id);
            Assert.AreEqual(456299, credit.ApplicationId);
            Assert.AreEqual("Debit", credit.Type);
            Assert.AreEqual("Payment", credit.Summary);
            Assert.AreEqual(95.11, credit.Amount);
        }
    }
}