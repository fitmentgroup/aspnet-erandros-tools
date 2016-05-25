using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetErandrosTools.Exceptions;
using AspNetErandrosTools.Extensions;

namespace AspNetErandrosTools.Services
{
    public class Request
    {
        public string BaseRoute { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public Request()
        {
            Headers = new Dictionary<string, string>();
        }

        public void AddAuthorizationCookie(string cookie)
        {
            Headers.Add("Authorization", cookie);
        }

        public async Task<Response> Send(string url, string method = "GET", HttpContent content = null, bool check = true)
        {
            var client = HttpClient();
            if (url[0] != '/') url = "/" + url;
            var message = new HttpRequestMessage(
                method: new HttpMethod(method),
                requestUri: BaseRoute + url
            );
            if (content != null) message.Content = content;
            var responseMsg = await client.SendAsync(message);
            var response = new Response(responseMsg);
            if (check)
            {
                Check(response, message);
            }
            return response;
        }

        public async Task<Response> Send(string url, string method, Stream content, string mediaType = "application/json", bool check = true)
        {
            StringContent stringContent = null;
            var _content = new StreamReader(content).ReadToEnd();
            if (!string.IsNullOrEmpty(_content))
            {
                var encoding = System.Text.Encoding.UTF8;
                stringContent = new StringContent(_content, encoding, mediaType);
            };
            return await Send(url, method, stringContent, check);
        }

        public async Task<Response> Send(string url, string method, object content, string mediaType = "application/json")
        {
            var json = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, mediaType);
            return await Send(url, method, stringContent);
        }

        /// <summary>
        /// Returns an HttpClient with the token cookie set
        /// </summary>
        private HttpClient HttpClient()
        {
            var req = new HttpClient();
            foreach(var header in Headers)
            {
                req.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            return req;
        }

        public async Task<Response<T>> Send<T>(string url, string method = "GET", HttpContent content = null, bool check = true)
        {
            var client = HttpClient();
            if (url[0] != '/') url = "/" + url;
            var message = new HttpRequestMessage(
                method: new HttpMethod(method),
                requestUri: BaseRoute + url
            );
            if (content != null) message.Content = content;
            var responseMsg = await client.SendAsync(message);
            var response = new Response<T>(responseMsg);
            if (check)
            {
                Check(response, message);
            }
            return response;
        }

        public async Task<Response<T>> Send<T>(string url, string method, Stream content, string mediaType = "application/json", bool check = true)
        {
            StringContent stringContent = null;
            var _content = new StreamReader(content).ReadToEnd();
            if (!string.IsNullOrEmpty(_content))
            {
                var encoding = System.Text.Encoding.UTF8;
                stringContent = new StringContent(_content, encoding, mediaType);
            };
            return await Send<T>(url, method, stringContent, check);
        }

        public async Task<Response<T>> Send<T>(string url, string method, object content, string mediaType = "application/json")
        {
            var json = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, mediaType);
            return await Send<T>(url, method, stringContent);
        }

        public void Check(Response response, HttpRequestMessage message)
        {
            if (!response.IsOK)
            {
                if (response.IsUnauthorized)
                    throw new UnauthorizedRequestException(message.RequestUri);
                else throw new Exception(
                    $"(Bad API request from Viper Client) code: {response.StatusCode}, to: {message.RequestUri}");
            }
        }

        public void Check<T>(Response<T> response, HttpRequestMessage message)
        {
            if (!response.IsOK)
            {
                if (response.IsUnauthorized)
                    throw new UnauthorizedRequestException(message.RequestUri);
                else throw new Exception(
                    $"(Bad API request from Viper Client) code: {response.StatusCode}, to: {message.RequestUri}");
            }
        }
    }
}
