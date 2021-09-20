using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Credit.Core.BLL;
using Credit.Core.Models;
using CreditWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditWebApp.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly IClientBL _clientBL;
		private readonly IMapper _mapper;

		public ClientsController(IClientBL clientBL, IMapper mapper)
		{
			this._clientBL = clientBL;
			this._mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> GetClients()
		{
			var clients = await _clientBL.GetClients(new ClientFilter());
			var clientModels = _mapper.Map<List<ClientModel>>(clients);
			return Ok(clientModels);
		}

		[Route("{id:int}")]
		[HttpGet]
		public async Task<ActionResult> GetClientById(int id)
		{
			int i = id;
			ClientModel client = new ClientModel { Id = i, Name = $"name{i}", Email = $"email{i}@gmail.com" };
			return Ok(client);
		}

		[HttpPut]
		public async Task<ActionResult> CreateClient(ClientModel clientModel)
		{
			if (!ModelState.IsValid) return Ok(ModelState.Values);
			int i = int.MaxValue;
			ClientModel client = new ClientModel { Id = i, Name = $"name{i}", Email = $"email{i}@gmail.com" };
			return Ok(client);
		}

		[HttpPost]
		public async Task<ActionResult> UpdateClient(ClientModel clientModel)
		{
			return Ok(clientModel);
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteClient(ClientModel clientModel)
		{
			return Ok();
		}
	}
}
