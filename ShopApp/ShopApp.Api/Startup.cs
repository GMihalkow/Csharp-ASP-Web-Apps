using Owin;
using ShopApp.Api.App_Start;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

namespace ShopApp.Api
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            // Registrating ninject IOC
            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());

            // Registrating filters
            FilterConfig.RegisterFilters(config.Filters);

            appBuilder.UseWebApi(config);

            config.EnsureInitialized();
        }
    }
}