using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Credit.Core.BLL;
using Credit.Core.Models;
using CreditWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CreditWebApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditController: ControllerBase
    {
    		private readonly ICreditBL  _creditBL;
            private readonly IMapper _mapper;

    		public CreditController(ICreditBL creditBL, IMapper mapper)
    		{
	            Log.Debug("Start CreditController...");
    			_creditBL = creditBL;
                _mapper = mapper;
            }
            
    		[HttpGet]
    		public async Task<ActionResult> GetCredits()
    		{
	            Log.Debug("Run GetCredits..");
	            var credits = await _creditBL.GetCredits(new CreditFilter());
                var creditModels = _mapper.Map<List<CreditModel>>(credits);
                Log.Debug("Found GetCredits {@creditModels.Count} count", creditModels.Count);
                return Ok(creditModels);
    		}
    
    		[Route("{id:Guid}")]
    		[HttpGet]
    		public async Task<ActionResult> GetCreditById(Guid id)
            {
	            Log.Debug("Run GetCreditById with {@Id}", id);
	            var credit = await _creditBL.GetCreditById(id);
	            var creditModels = _mapper.Map<CreditModel>(credit);
	            Log.Debug("GetCreditById result for {@Id} is {@CreditModels}", id, creditModels);
	            return Ok(creditModels);
    		}
    
    		[HttpPut]
    		public async Task<ActionResult> CreateCredit(CreditModel creditModel)
    		{
	            Log.Debug("Run CreateCredit with {@CreditModel}", creditModel);
	            if (!ModelState.IsValid) return Ok(ModelState.Values);
	            var credit = _mapper.Map<Credit.Core.Models.Credit>(creditModel);
	            var newCredit = await _creditBL.CreateCredit(credit);
	            var newCreditModel = _mapper.Map<CreditModel>(newCredit);
	            return Ok(newCreditModel);
            }
    
    		[HttpPost]
    		public async Task<ActionResult> UpdateCredit(CreditModel creditModel)
    		{
	            Log.Debug("Run UpdateCredit with {@CreditModel}", creditModel);
	            if (!ModelState.IsValid) return Ok(ModelState.Values);
	            var credit = _mapper.Map<Credit.Core.Models.Credit>(creditModel);
	            var newCredit = await _creditBL.UpdateCredit(credit);
	            var newCreditModel = _mapper.Map<CreditModel>(newCredit);
	            return Ok(newCreditModel);
    		}
    
    		[HttpDelete]
    		public async Task<ActionResult> DeleteCredit(Guid id)
            {
	            Log.Debug("Run DeleteCredit with {@Id}",id);
	            await _creditBL.DeleteCredit(id);
    			return Ok();
    		}
    
    }
}