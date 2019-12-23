using ShopApp.Api.Infrastructure.ContentNegotiators;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ShopApp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute("http://localhost:50614", "*", "*"));

            // configuring the web api to return only json data

            var jsonFormatter = new JsonMediaTypeFormatter();

            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);

            // optimizing the browser negotiation process to skip the unnecassery actions
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}