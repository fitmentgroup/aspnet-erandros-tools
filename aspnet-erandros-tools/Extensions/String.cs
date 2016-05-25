using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class StringExtensions
    {
        public static Boolean LowerCaseEquals(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
        public static string NotEmptyThen(this string a, string b)
        {
            return a.Length > 0 ? b : "";
        }
        public static Boolean Empty(this string a)
        {
            return string.IsNullOrEmpty(a);
        }
        public static string PascalFormat(this string a)
        {
            return Regex.Replace(
                Regex.Replace(
                    a,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
    }
}
