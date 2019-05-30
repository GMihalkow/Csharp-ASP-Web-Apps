using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Data.EntityConfiguration;
using SimpleShop.Data.Models;

namespace Data.SimpleShop.Data
{
    public class SimpleShopDbContext : IdentityDbContext
    {
        public SimpleShopDbContext(DbContextOptions<SimpleShopDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<Message> Messages { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new CategoryConfiguration());

            builder
                .ApplyConfiguration(new MessageConfiguration());

            builder
                .ApplyConfiguration(new OrderConfiguration());

            builder
                .ApplyConfiguration(new ProductConfiguration());

            builder
                .ApplyConfiguration(new ConversationConfiguration());
        }
    }
}
