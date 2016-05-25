using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Services
{
    public class ScopedModel<T> where T : new()
    {
        private T Model { get; set; }
        private string Key { get; set; }

        public ScopedModel(string key)
        {
            Key = key;
        }

        public T Get()
        {
            if (Model == null)
            {
                Model = new T();
            }
            return Model;
        }

        public void Set(T model)
        {
            Model = model;
        }
    }
}
