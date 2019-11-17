using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;

namespace Louis
{
    public class LogAttribute : ActionFilterAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
     
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _logger.Info("Action Method {0} executing at {1}", actionContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString());
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _logger.Info("Action Method {0} executed at {1}", actionExecutedContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString());
        }
    }
}
