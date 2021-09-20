using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Credit.Core.DAL;
using Credit.Core.Models;
using Newtonsoft.Json;

namespace Credit.MockDAL
{
    public class MockCreditDataRepository: ICreditDataRepository
    {
        private readonly List<Credit.Core.Models.Credit> _credits;
        
        public MockCreditDataRepository()
        {
            const string filename = "data.json";
            _credits = JsonConvert.DeserializeObject<List<Credit.Core.Models.Credit>>(File.ReadAllText(filename));
        }
        public Task<List<Credit.Core.Models.Credit>> GetCredits(CreditFilter filter)
        {
            var query = _credits.AsQueryable();

            if (filter.PostingDateFrom.HasValue)
                query = query.Where(x => x.PostingDate >= filter.PostingDateFrom);
            if (filter.PostingDateTo.HasValue)
                query = query.Where(x => x.PostingDate <= filter.PostingDateTo);
            if(!string.IsNullOrEmpty(filter.Type))
                query = query.Where(x => x.Type.Equals(filter.Type));
            if(filter.Amount>0)
                query = query.Where(x => x.Amount == filter.Amount);
            if(filter.ApplicationId>0)
                query = query.Where(x => x.ApplicationId == filter.ApplicationId);

            return Task.FromResult(query.Take(filter.Take).Skip(filter.Skip).ToList()) ;
        }

        public Task<Credit.Core.Models.Credit> GetCreditById(Guid id)
        {
            return Task.FromResult(_credits.SingleOrDefault(c => c.Id.Equals(id)));
        }

        public Task<Credit.Core.Models.Credit> CreateCredit(Credit.Core.Models.Credit credit)
        {
            var newCredit = Credit.Core.Services.MapProfile.Instance().Map<Credit.Core.Models.Credit>(credit);
            newCredit.Id = Guid.NewGuid();
            _credits.Add(newCredit);
            return Task.FromResult(newCredit);
        }

        public Task<Credit.Core.Models.Credit> UpdateCredit(Credit.Core.Models.Credit credit)
        {
            var found = _credits.SingleOrDefault(c => c.Id == credit.Id);
            if (found == null)
                throw new ArgumentOutOfRangeException($"Credit id:{credit.Id} didn`t find.");
            _credits.Remove(found);
            _credits.Add(credit);
            return Task.FromResult(credit);
        }

        public Task DeleteCredit(Guid credit)
        {
            _credits.RemoveAll(c => c.Id == credit);
            return Task.CompletedTask;
        }
    }
}