using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Services
{
    public class SessionBoolean
    {
        private ISession session { get; set; }
        private string Key { get; set; }

        public SessionBoolean(IHttpContextAccessor accessor, string key)
        {
            session = accessor.HttpContext.Session;
            Key = key;
        }

        public bool Get()
        {
            var val = session.GetString(Key);
            val = string.IsNullOrEmpty(val) ? "false" : val;
            return Convert.ToBoolean(val);
        }

        public void Set(bool value)
        {
            session.SetString(Key, value.ToString());
        }
    }
}
