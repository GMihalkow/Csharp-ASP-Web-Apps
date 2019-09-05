using ShopApp.Models;
using System.Data.Entity.ModelConfiguration;

namespace ShopApp.Data.EntityConfiguration.User
{
	public class ShopUserConfiguration : EntityTypeConfiguration<ShopUser>
	{
		public ShopUserConfiguration()
		{
			this.HasKey(u => u.Id);

			this.HasMany(u => u.Categories)
				.WithRequired(c => c.Creator)
				.HasForeignKey<string>(c => c.CreatorId)
				.WillCascadeOnDelete(false);

            this.HasMany(u => u.Products)
                .WithMany(p => p.Users)
                .Map(order =>
                {
                    order.MapRightKey ("ProductId");
                    order.MapLeftKey("UserId");
                    order.ToTable("Orders");
                });
		}
	}
}