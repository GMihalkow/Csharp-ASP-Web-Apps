using Microsoft.AspNet.Identity.EntityFramework;
using ShopApp.Data.EntityConfiguration.Product;
using ShopApp.Data.EntityConfiguration.User;
using ShopApp.Models;
using System.Data.Entity;

namespace ShopApp.Data
{
	public class ShopAppDbContext : IdentityDbContext<ShopUser>
	{
		public ShopAppDbContext() 
			: base("DbConnection")
		{
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }
		
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new ProductConfiguration());
			modelBuilder.Configurations.Add(new ShopUserConfiguration());
		}

		public static ShopAppDbContext Create()
		{
			return new ShopAppDbContext();
		}
	}
}
