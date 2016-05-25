using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class BooleanExtensions
    {
        public static string TrueThen(this Boolean b, string s)
        { return b ? s : ""; }
        public static string FalseThen(this Boolean b, string s)
        { return b ? "" : s; }
    }
}
