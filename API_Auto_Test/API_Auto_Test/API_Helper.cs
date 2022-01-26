using RestSharp;
using System.Collections.Generic;
using System;
using Xunit;
using OpenQA.Selenium;

namespace API_Auto_Test
{
    public static class API_Helper
    {
        public static IRestResponse SendJSON_API_Request(string url, string headerName, string headerValue, Dictionary<string,string> body)
        {
            RestClient client = new RestClient(baseUrl: url)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader(name: headerName, value: headerValue);
            foreach(var data in body)
            {
                request.AddParameter(data.Key, data.Value);
            }
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            return response;
        }
        public static List<Cookie> ExtractCookie(IRestResponse response)
        {
            List<Cookie> allcookies = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                allcookies.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));
            return allcookies;
        }
    }
}
