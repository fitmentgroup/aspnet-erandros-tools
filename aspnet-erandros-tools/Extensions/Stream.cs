using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Extensions
{
    public static class StreamExtensions
    {
        public static dynamic Json(this Stream s)
        {
            return JsonConvert.DeserializeObject(new StreamReader(s).ReadToEnd());
        }
    }
}
