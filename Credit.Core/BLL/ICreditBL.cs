using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Credit.Core.Models;

namespace Credit.Core.BLL
{
    public interface ICreditBL
    {
        public Task<List<Models.Credit>> GetCredits(CreditFilter filter);
        public Task<Models.Credit> GetCreditById(Guid id);
        public Task<Models.Credit> CreateCredit(Models.Credit credit);
        public Task<Models.Credit> UpdateCredit(Models.Credit credit);
        public Task DeleteCredit(Guid id);
    }
}