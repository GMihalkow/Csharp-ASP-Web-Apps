[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ShopApp.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ShopApp.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ShopApp.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;
    using Services.Account;
    using Services.Account.Contracts;
    using Services.Category;
    using Services.Category.Contracts;
    using ShopApp.Data;
    using ShopApp.Models;
    using ShopApp.Web.Models;
    using ShopApp.Web.Repositories;
    using ShopApp.Web.Repositories.Contracts;
    using ShopApp.Web.Services.Order;
    using ShopApp.Web.Services.Order.Contracts;
    using ShopApp.Web.Services.Product;
    using ShopApp.Web.Services.Product.Contracts;
    using System;
    using System.Web;
    using System.Web.Http;

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

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            kernel.Bind<ShopAppDbContext>().To<ShopAppDbContext>().InRequestScope();
            kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
        }
    }
}
