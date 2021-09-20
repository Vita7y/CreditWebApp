using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credit.Core.BLL;
using Credit.Core.DAL;
using Credit.Core.Models;

namespace Credit.BLL
{
	public class ClientBL : IClientBL
	{
		private readonly IClientDataRepository _clientDataRepository;

		public ClientBL(IClientDataRepository clientDataRepository)
		{
			this._clientDataRepository = clientDataRepository;
		}

		public async Task<Client> CreateClient(Client client)
		{
			if (client.Age <= 18)
				throw new NotSupportedException("idi lesom");

			var dbClient = await _clientDataRepository.CreateClient(client);
			return dbClient;
		}

		public async Task DeleteClient(Client client)
		{
			await _clientDataRepository.DeleteClient(client);
		}

		public async Task<Client> GetClientById(int id)
		{
			return await _clientDataRepository.GetClientById(id);
		}

		public async Task<List<Client>> GetClients(ClientFilter filter)
		{
			if (filter.Take > 1000)
				filter.Take = 100;

			return await _clientDataRepository.GetClients(filter);
		}

		public async Task<List<Client>> GetLastClients()
		{
			ClientFilter filter = new ClientFilter();
			return await _clientDataRepository.GetClients(filter);
		}

		public async Task<Client> UpdateClient(Client client)
		{
			return await _clientDataRepository.UpdateClient(client);
		}
	}
}
