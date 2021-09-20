using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Credit.BLL;
using Credit.Core.Models;
using CreditWebApp.Controllers;
using CreditWebApp.Models;
using CreditWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Credit.MockDAL;
using Assert = NUnit.Framework.Assert;
namespace Credit.Tests
{
    public class CreditControllerIntegrationTests
    {
        private CreditController _controller;
        
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MapProfile()); });
            var mapper = new Mapper(config);
            var dataRepository = new MockCreditDataRepository();
            var creditBL = new CreditBL(dataRepository);
            _controller = new CreditController(creditBL, mapper);
        }
        
        [Test]
        public async Task Test_CreateCredit_Pass()
        {
            var credit = new CreditModel()
            {
                Amount = 123, Summary = "Payment", Type = "TestType", ApplicationId = 101
            };
            
            var result = await _controller.CreateCredit(credit);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var viewResult = result as OkObjectResult;
            Assert.IsInstanceOf<CreditModel>(viewResult.Value);
            var newCredit = viewResult.Value as CreditModel;
            
            Assert.IsNotNull(newCredit);
            Assert.AreEqual(123, newCredit.Amount);
            Assert.AreEqual("Payment", newCredit.Summary);
            Assert.AreEqual(101, newCredit.ApplicationId);
            Assert.AreEqual("TestType", newCredit.Type);
            
            await _controller.DeleteCredit(newCredit.Id);
        }

        [Test]
        public async Task Test_UpdateCredit_Pass()
        {
            var guid = Guid.Parse("d2032222-47a6-4048-9894-11ab8ebb9f69");
            var result = await _controller.GetCreditById(guid) as OkObjectResult;
            var credit = result.Value as CreditModel; 
            
            credit.Amount = 123;
            credit.Summary = "TestPayment";
            credit.Type = "TestType";
            credit.ApplicationId = 101;
            
            result = await _controller.UpdateCredit(credit) as OkObjectResult;
            var updateCredit = result.Value as CreditModel; 
            
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
            var credit = await _controller.GetCreditById(guid) as OkObjectResult;
            Assert.IsNotNull(credit.Value);
            
            await _controller.DeleteCredit(guid);
            
            credit = await _controller.GetCreditById(guid) as OkObjectResult;
            Assert.IsNull(credit.Value);
        }

        [Test]
        public async Task Test_GetCredits_NotEmpty()
        {
            var result = await _controller.GetCredits() as OkObjectResult;
            var credits = result.Value as List<CreditModel>;

            Assert.IsNotEmpty(credits);
        }

        [Test]
        public async Task Test_GetCreditById_Pass()
        {
            var guid = Guid.Parse("7a66f608-2979-4b79-ba2e-d9b4d573b294");
            var result = await _controller.GetCreditById(guid) as OkObjectResult;
            var credit = result.Value as CreditModel;

            Assert.AreEqual(guid, credit.Id);
            Assert.AreEqual(456299, credit.ApplicationId);
            Assert.AreEqual("Debit", credit.Type);
            Assert.AreEqual("Payment", credit.Summary);
            Assert.AreEqual(95.11, credit.Amount);
        }
    }
}