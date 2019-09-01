using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace ShopApp.Models
{
	public class ShopUser : IdentityUser
	{
		public override string Id { get; set; }
		public override string UserName { get; set; }
		public override string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime RegisteredOn { get; set; }
		public ICollection<Category> Categories { get; set; }
		public ICollection<Product> BoughtProducts { get; set; }
	}
}