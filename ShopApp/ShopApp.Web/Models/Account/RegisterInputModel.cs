using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
	public class RegisterInputModel
	{
		[Required]
		[MinLength(6)]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[MinLength(6)]
		public string FirstName { get; set; }

		[MinLength(6)]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(ConfirmPassword))]
		public string Password { get; set; }

		[Required]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		public DateTime BirthDate { get; set; }
	}
}