using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Core.Models
{
	public  class ClientFilter
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public int? AgeStart { get; set; }
		public int? AgeEnd { get; set; }
		
		public int Take{ get; set; } = 1000;
		public int Skip { get; set; }
	}
}
