namespace ShopApp.Models
{
	public class ShopUserProduct : BaseEntity<string>
	{
		public string ProductId { get; set; }
		public Product Product { get; set; }
		public string UserId { get; set; }
		public ShopUser User { get; set; }
	}
}