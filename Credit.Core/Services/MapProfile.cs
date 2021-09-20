using System.ComponentModel.Design.Serialization;
using AutoMapper;

namespace Credit.Core.Services
{
	public class MapProfile : Profile
	{
		private MapProfile()
		{
			CreateMap<Core.Models.Credit, Core.Models.Credit>().ForMember(rec => rec.Id, opt => opt.Ignore());
		}

		private static IMapper _mapper;
		public static IMapper Instance()
		{
			if (_mapper == null)
			{
				var config = new MapperConfiguration(cfg => {
					cfg.AddProfile(new MapProfile()); });
				_mapper = new Mapper(config);
			}
			return _mapper;
		}
	}
}
