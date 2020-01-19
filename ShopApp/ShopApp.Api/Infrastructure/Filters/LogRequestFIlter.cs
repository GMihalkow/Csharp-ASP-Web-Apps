using Microsoft.Owin;
using SimpleLogger;
using SimpleLogger.Contracts;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShopApp.Api.Infrastructure.Filters
{
    public class LogRequestFIlter : ActionFilterAttribute
    {
        private readonly ILogManager logManager;

        public LogRequestFIlter()
        {
            this.logManager = new LogManager(); 
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var owinContext = actionContext.Request.Properties["MS_OwinContext"] as OwinContext;
            
            this.logManager.ConsoleLog($"IP: {owinContext.Request.RemoteIpAddress} Method: {actionContext.Request.Method} Host: {actionContext.Request.Headers.Host} Route: {actionContext.Request.RequestUri.PathAndQuery}");
        }
    }
}