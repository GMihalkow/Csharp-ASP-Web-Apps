using Data.SimpleShop.Data;

namespace SimpleShop.DataServices.Interfaces.Db
{
    public interface IDbService
    {
        SimpleShopDbContext DbContext { get; }
    }
}