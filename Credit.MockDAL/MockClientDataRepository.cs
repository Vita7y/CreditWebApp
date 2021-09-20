using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credit.Core.DAL;
using Credit.Core.Models;

namespace Credit.MockDAL
{
	public class MockClientDataRepository : IClientDataRepository
	{
		private static readonly List<Client> Clients = new List<Client>();

		static MockClientDataRepository()
		{
			for (int i = 0; i < 10; i++)
			{
				Clients.Add(new Client { Id = i, Name = $"name{i}", Email = $"aa{i}@bb.com", Age = i * 10 });
			}
		}


		public Task<Client> CreateClient(Client client)
		{
			int nextId = Clients.Count;
			client.Id = nextId;
			Clients.Add(client);
			return Task.FromResult(client);
		}

		public Task DeleteClient(Client client)
		{
			Client found = Clients.SingleOrDefault(x => x.Id == client.Id);
			if (found == null)
				throw new ArgumentOutOfRangeException("");
			Clients.Remove(found);
			return Task.CompletedTask;
		}

		public Task<Client> GetClientById(int id)
		{
			Client found = Clients.SingleOrDefault(x => x.Id == id);
			return Task.FromResult(found);
		}

		public Task<List<Client>> GetClients(ClientFilter filter)
		{
			var query = Clients.AsQueryable();

			if (filter.AgeEnd.HasValue)
				query = query.Where(x => x.Age <= filter.AgeEnd);
			if (filter.AgeStart.HasValue)
				query = query.Where(x => x.Age >= filter.AgeStart);


			return Task.FromResult(query.Take(filter.Take).Skip(filter.Skip).ToList()) ;
		}

		public Task<Client> UpdateClient(Client client)
		{
			Client found = Clients.SingleOrDefault(x => x.Id == client.Id);
			if (found == null)
				throw new ArgumentOutOfRangeException("");
			Clients.Remove(found);
			Clients.Add(client);
			return Task.FromResult(client);
		}
	}
}
