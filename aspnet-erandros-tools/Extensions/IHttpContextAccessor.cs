using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class IHttpContextAccessorExtensions
    {
        public static T Service<T>(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.ApplicationServices.GetRequiredService<T>();
        }
    }
}
