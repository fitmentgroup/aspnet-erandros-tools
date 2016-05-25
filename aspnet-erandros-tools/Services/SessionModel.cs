using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Services
{
    public class SessionModel<T> where T : new()
    {
        private ISession session { get; set; }
        private T Model { get; set; }
        private List<string> Fields { get; set; }
        private string Key { get; set; }
        private IHostingEnvironment Env { get; set; }

        public SessionModel(IHttpContextAccessor accessor, IHostingEnvironment env,
            IConfiguration config)
        {
            session = accessor.HttpContext.Session;
            Env = env;
            Config = config;
        }

        public SessionModel<T> Setup(string key, string fields)
        {
            Key = key;
            Fields = fields.Split(',').ToList();
            return this;
        }

        public T Get
        {
            get
            {
                if (Model == null)
                {
                    Model = new T();
                    foreach (var field in Fields)
                    {
                        PropertyInfo prop = Model.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
                        dynamic val = null;
                        if (prop.PropertyType == typeof(Int32))
                            val = session.GetInt32(Key + field);
                        else val = session.GetString(Key + field);
                        if (Env.IsDevelopment())
                        {
                            var _val = Config.GetSection(Key + field).Value;
                            if (!string.IsNullOrEmpty(_val))
                            {
                                val = _val;
                            }
                        }
                        prop.SetValue(Model, val, null);
                    }
                }
                return Model;
            }
        }

        public IConfiguration Config { get; private set; }

        public void Set(T model)
        {
            Model = model;
            foreach(var field in Fields)
            {
                PropertyInfo prop = Model.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
                if (prop.PropertyType == typeof(Int32))
                    session.SetInt32(Key + field, Convert.ToInt32(prop.GetValue(Model)));
                else session.SetString(Key + field, prop.GetValue(Model).ToString());
            }
        }
    }
}
