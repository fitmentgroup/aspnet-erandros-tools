using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNetErandrosTools.Extensions
{
    public static class ControllerExtensions
    {
        public static T Service<T>(this Microsoft.AspNet.Mvc.Controller controller)
        {
            return controller.HttpContext.ApplicationServices.GetService<T>();
        }
    }
}
