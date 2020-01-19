using SimpleLogger;
using SimpleLogger.Contracts;
using System;
using System.Web.Http.Filters;

namespace ShopApp.Api.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogManager logManager;

        public LogExceptionFilter()
        {
            this.logManager = new LogManager();
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            this.logManager.GlobalExceptionLog(actionExecutedContext.Exception, $"Method: {actionExecutedContext.Request.Method} Route: {actionExecutedContext.Request.RequestUri.PathAndQuery}");
        }
    }
}