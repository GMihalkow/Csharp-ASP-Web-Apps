using System.Data.Entity.ModelConfiguration;

namespace ShopApp.Data.EntityConfiguration.Product
{
	public class ProductConfiguration : EntityTypeConfiguration<Models.Product>
	{
		public ProductConfiguration()
		{
			this.HasKey(p => p.Id);

			this.HasRequired(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.WillCascadeOnDelete(true);
		}
	}
}