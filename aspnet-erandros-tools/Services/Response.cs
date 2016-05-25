using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Services
{
    public class BaseResponse
    {
        public HttpResponseMessage Message { get; set; }

        public BaseResponse(HttpResponseMessage message)
        {
            Message = message;
        }

        public int StatusCode
        {
            get { return (int)Message.StatusCode; }
        }

        public bool IsOK
        {
            get
            {
                var code = (int)Message.StatusCode;
                return (code >= 200 && code < 300);
            }
        }

        public bool IsUnauthorized
        {
            get
            {
                return Message.StatusCode == HttpStatusCode.Unauthorized;
            }
        }
    }

    public class Response : BaseResponse
    {
        public Response(HttpResponseMessage message) : base(message)
        {
        }

        public dynamic Json
        {
            get
            {
                try
                {
                    var json = Message.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject(json);
                }
                catch (Exception) { return null; }
            }
        }
    }

    public class Response<T> : BaseResponse
    {
        public Response(HttpResponseMessage message) :base(message)
        {
        }

        public T Data
        {
            get
            {
                try
                {
                    var json = Message.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception) { return default(T); }
            }
        }
    }
}
