using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Credit.Core.BLL;
using Credit.Core.DAL;
using Credit.Core.Models;

namespace Credit.BLL
{
    public class CreditBL: ICreditBL
    {
        private readonly ICreditDataRepository _dataRepository;

        public CreditBL(ICreditDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        public async Task<List<Core.Models.Credit>> GetCredits(CreditFilter filter)
        {
            var credits = await _dataRepository.GetCredits(filter);
            return credits;
        }

        public async Task<Core.Models.Credit> GetCreditById(Guid id)
        {
            var credit = await _dataRepository.GetCreditById(id);
            return credit;
        }

        public async Task<Core.Models.Credit> CreateCredit(Core.Models.Credit credit)
        {
            var creditExist = await _dataRepository.GetCreditById(credit.Id);
            if (creditExist != null)
                return creditExist;
            var newCredit = await _dataRepository.CreateCredit(credit);
            return newCredit;
        }

        public async Task<Core.Models.Credit> UpdateCredit(Core.Models.Credit credit)
        {
            var creditExist = await _dataRepository.GetCreditById(credit.Id);
            if (creditExist == null)
                return await CreateCredit(credit);
            return await _dataRepository.UpdateCredit(credit);
        }

        public async Task DeleteCredit(Guid id)
        {
            var creditExist = await _dataRepository.GetCreditById(id);
            if (creditExist == null)
                throw new DataException($"Credit {id} does`t exist.");
            await _dataRepository.DeleteCredit(id);
        }
    }
}