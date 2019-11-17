using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Louis
{
    public class LouisExceptionFilter : ExceptionFilterAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception; 
            string stack = context.Exception.StackTrace;
            _logger.Error(ex, $"{ex.Message} at:{stack}");

            base.OnException(context);
        }
    }
}
