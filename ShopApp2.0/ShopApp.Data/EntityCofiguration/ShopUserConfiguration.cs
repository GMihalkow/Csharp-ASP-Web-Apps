using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Models;

namespace ShopApp.Data.EntityCofiguration
{
    public class ShopUserConfiguration : IEntityTypeConfiguration<ShopUser>
    {
        public void Configure(EntityTypeBuilder<ShopUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Categories)
                .WithOne(c => c.Creator)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}