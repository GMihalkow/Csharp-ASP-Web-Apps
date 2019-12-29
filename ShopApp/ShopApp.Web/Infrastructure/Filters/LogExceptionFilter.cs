using SimpleLogger;
using SimpleLogger.Contracts;
using System.Web.Mvc;

namespace ShopApp.Web.Infrastructure.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly ILogManager logManager;

        public LogExceptionFilter()
        {
            this.logManager = new LogManager();
        }

        public void OnException(ExceptionContext filterContext)
        {
            this.logManager.Log(filterContext.Exception);
        }
    }
}