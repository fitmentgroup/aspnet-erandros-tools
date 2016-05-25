using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class FilterContextExtensions
    {
        public static T Service<T>(this FilterContext context)
        {
            return context.HttpContext.ApplicationServices.GetService<T>();
        }

        public static void RedirectToLogin(this ActionExecutingContext context)
        {
            var result = new RedirectToActionResult("LogIn", "Account",
                new RouteValueDictionary(new
                {
                    returnUrl = context.HttpContext.Request.Path
                }));
            dynamic controller = context.Controller;
            controller.TempData["LoginError"] = "There was an unauthorized request.  Please re-enter your login credentials";
            context.Result = result;
        }
    }
}
