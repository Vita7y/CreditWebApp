using System.ComponentModel.DataAnnotations;

namespace CreditWebApp.Models
{
	public class ClientModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
