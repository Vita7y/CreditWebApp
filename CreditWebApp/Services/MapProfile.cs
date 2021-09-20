using AutoMapper;
using Credit.Core.Models;
using CreditWebApp.Models;

namespace CreditWebApp.Services
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<ClientModel, Client>();
			CreateMap<Client, ClientModel>();
			CreateMap<CreditModel, Credit.Core.Models.Credit>();
			CreateMap<Credit.Core.Models.Credit, CreditModel>();
		}
	}
}
