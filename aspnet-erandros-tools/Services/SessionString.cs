using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;

namespace AspNetErandrosTools.Services
{
    public class SessionString
    {
        private ISession session { get; set; }
        private string Key { get; set; }
        private IHostingEnvironment Env { get; set; }
        private IConfiguration Config { get; set; }

        public SessionString(IHttpContextAccessor accessor, IHostingEnvironment env,
            IConfiguration config)
        {
            session = accessor.HttpContext.Session;
            Env = env;
            Config = config;
        }

        public SessionString Setup(string key)
        {
            Key = key;
            return this;
        }

        public string Get()
        {
            if (Env.IsDevelopment())
            {
                var token = Config.GetSection(Key).Value;
                if (!string.IsNullOrEmpty(token))
                {
                    return token;
                }
            }
            return session.GetString(Key);
        }

        public void Set(string value)
        {
            session.SetString(Key, value);
        }
    }
}
