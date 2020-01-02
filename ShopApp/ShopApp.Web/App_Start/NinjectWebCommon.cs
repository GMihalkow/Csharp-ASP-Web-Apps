[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ShopApp.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ShopApp.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ShopApp.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Services.Account;
    using Services.Account.Contracts;
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
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // injecting the repositories
            kernel.Bind<IRepository<CategoryViewModel, CategoryInputModel>>().To<CategoryRepository>().InRequestScope();
            kernel.Bind<IRepository<ProductViewModel, ProductBaseInputModel>>().To<ProductRepository>().InRequestScope();
            
            // injecting the services
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();

            // injecting the custom logger
            kernel.Bind<ILogManager>().To<LogManager>().InSingletonScope();
        }
    }
}