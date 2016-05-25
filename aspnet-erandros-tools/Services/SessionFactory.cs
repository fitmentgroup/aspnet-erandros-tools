using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using Microsoft.Extensions.Configuration;

namespace AspNetErandrosTools.Services
{
    public class SessionFactory
    {
        private IHostingEnvironment Env { get; set; }
        private IHttpContextAccessor Accessor { get; set; }
        private IConfiguration Config { get; set; }

        public SessionFactory(IHostingEnvironment env, IHttpContextAccessor accessor,
            IConfiguration config)
        {
            Env = env;
            Accessor = accessor;
            Config = config;
        }
        public SessionModel<T> CreateSessionModel<T>() where T : new()
        {
            return new SessionModel<T>(Accessor, Env, Config);
        }
        public SessionBoolean CreateSessionBoolean(string key)
        {
            return new SessionBoolean(Accessor, key);
        }
        public SessionString CreateSessionString()
        {
            return new SessionString(Accessor, Env, Config);
        }
        public SessionJson<T> CreateSessionJson<T>(string key) where T : new()
        {
            return new SessionJson<T>(Accessor, key);
        }
    }
}
