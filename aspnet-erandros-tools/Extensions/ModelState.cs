using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class ModelStateExtensions
    {
        public static string Errors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return string.Join("\n", errors);
        }
    }
}
