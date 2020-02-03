using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
	public class LoginInputModel
	{
		[Required]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}