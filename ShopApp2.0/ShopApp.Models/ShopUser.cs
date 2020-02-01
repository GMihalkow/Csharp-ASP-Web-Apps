using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ShopApp.Models
{
	public class ShopUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime RegisteredOn { get; set; }
		public ICollection<Category> Categories { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}