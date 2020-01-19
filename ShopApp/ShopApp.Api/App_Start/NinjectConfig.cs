using Ninject;
using Ninject.Web.Common;
using ShopApp.Dal;
using ShopApp.Dal.Repositories;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Dal.Services.Order;
using ShopApp.Dal.Services.Order.Contracts;
using ShopApp.Dal.Services.Product;
using ShopApp.Dal.Services.Product.Contracts;
using ShopApp.Dal.Services.Role;
using ShopApp.Dal.Services.Role.Contracts;
using ShopApp.Dal.Services.User;
using ShopApp.Dal.Services.User.Contracts;
using SimpleLogger;
using SimpleLogger.Contracts;

namespace ShopApp.Api.App_Start
{
    public class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            RegisterServices(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            // injecting the repositories
            kernel.Bind<IRepository<CategoryViewModel, CategoryInputModel>>().To<CategoryRepository>().InRequestScope();
            kernel.Bind<IRepository<ProductViewModel, ProductBaseInputModel>>().To<ProductRepository>().InRequestScope();

            // injecting the services
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();

            // injecting the custom logger
            kernel.Bind<ILogManager>().To<LogManager>().InSingletonScope();
        }
    }
}