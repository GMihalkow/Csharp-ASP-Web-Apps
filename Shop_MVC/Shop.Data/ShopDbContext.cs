using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityConfiguration;
using Shop.Data.Models;

namespace Shop.Data
{
    public class ShopDbContext : IdentityDbContext<ShopUser>
    {
        public ShopDbContext()
        {
        }

        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ShopMVC_DB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new ProductConfiguration());

            modelBuilder
                .ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
