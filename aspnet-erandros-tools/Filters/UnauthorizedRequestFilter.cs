using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetErandrosTools.Exceptions;
using AspNetErandrosTools.Extensions;

namespace AspNetErandrosTools.Filters
{
    public class UnauthorizedRequestFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedRequestException)
            {
                var logger = context.Service<ILogger<UnauthorizedRequestFilter>>();
                logger.LogWarning(context.Exception.Message);
                var result = new RedirectToActionResult("LogIn", "Account",
                    new RouteValueDictionary(new
                    {
                        returnUrl = context.HttpContext.Request.Path
                    }));
                context.Result = result;
            }
        }
    }
}
