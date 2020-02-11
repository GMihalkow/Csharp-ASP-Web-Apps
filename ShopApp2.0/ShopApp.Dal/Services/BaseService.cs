using ShopApp.Data;

namespace ShopApp.Dal.Services
{
    public abstract class BaseService
    {
        protected readonly ShopAppDbContext _dbContext;

        protected BaseService(ShopAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}