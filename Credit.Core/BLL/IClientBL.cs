using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credit.Core.Models;

namespace Credit.Core.BLL
{
	public interface IClientBL
	{
		public Task<List<Client>> GetClients(ClientFilter filter);
		public Task<List<Client>> GetLastClients();
		public Task<Client> GetClientById(int id);
		public Task<Client> CreateClient(Client client);
		public Task<Client> UpdateClient(Client client);
		public Task DeleteClient(Client client);
	}
}
