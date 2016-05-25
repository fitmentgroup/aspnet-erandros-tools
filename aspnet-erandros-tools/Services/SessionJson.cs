using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;

namespace AspNetErandrosTools.Services
{
    public class SessionJson<T> where T : new()
    {
        private ISession Session { get; set; }
        private T Model { get; set; }
        private string Key { get; set; }

        public SessionJson(IHttpContextAccessor accessor, string key)
        {
            Session = accessor.HttpContext.Session;
            Key = key;
        }

        public T Get()
        {
            if (Model == null)
            {
                var val = Session.GetString(Key);
                if (val != null)
                {
                    Model = JsonConvert.DeserializeObject<T>(val);
                }
                else
                {
                    Model = new T();
                }
            }
            return Model;
        }

        public void Set(T model)
        {
            Model = model;
            Session.SetString(Key, JsonConvert.SerializeObject(model));
        }
    }
}
