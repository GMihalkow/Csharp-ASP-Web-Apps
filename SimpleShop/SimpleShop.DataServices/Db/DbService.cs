using Data.SimpleShop.Data;
using SimpleShop.DataServices.Interfaces.Db;

namespace SimpleShop.DataServices.Db
{
    public class DbService : IDbService
    {
        public DbService(SimpleShopDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public SimpleShopDbContext DbContext { get; }
    }
}