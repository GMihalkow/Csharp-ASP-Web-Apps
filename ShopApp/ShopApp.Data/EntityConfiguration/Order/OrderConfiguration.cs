using System.Data.Entity.ModelConfiguration;

namespace ShopApp.Data.EntityConfiguration.Order
{
    public class OrderConfiguration : EntityTypeConfiguration<Models.Order>
    {
        public OrderConfiguration()
        {
            this.HasKey(order => order.Id);

            this.HasOptional(order => order.Product)
                .WithMany(product => product.Orders)
                .HasForeignKey(order => order.ProductId);

            this.HasOptional(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId);
        }
    }
}