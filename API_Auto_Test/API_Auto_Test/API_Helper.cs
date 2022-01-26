using RestSharp;
using System.Collections.Generic;
using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API_Auto_Test
{
    public static class API_Helper
    {
        public static IRestResponse SendJSON_API_Request(object body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(baseUrl: link)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            bool isBodyJson = false;
            foreach (var header in headers)
            {
                if (header.Value.Contains("application/json"))
                {
                    isBodyJson = true;
                    break;
                }
            }
            if (!isBodyJson)
            {
                foreach (var data in (Dictionary<string, string>)body)
                {
                    request.AddParameter(data.Key, data.Value);
                }
            }
            else
            {
                request.AddJsonBody(body);
                request.RequestFormat = DataFormat.Json;
            }
            IRestResponse response = client.Execute(request);
            return response;
        }
        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie res = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    res = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return res;
        }
        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> res = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                res.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));

            return res;
        }
        public static void Upload(Method type)
        {
            //RestClient restClient = new RestClient("http://stackoverflow.com/");
            //RestRequest restRequest = new RestRequest("/images");
            //restRequest.RequestFormat = DataFormat.Json;
            //restRequest.Method = Method.POST;
            //restRequest.AddHeader("Authorization", "Authorization");
            //restRequest.AddHeader("Content-Type", "multipart/form-data");
            //restRequest.AddFile("content", "/Users/hitsa/Desktop/Авы/wf.jpg");
            //var response = restClient.Execute(restRequest);
            //IWebDriver driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("http://stackoverflow.com/");
            //driver.Manage().Window.Maximize();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://imgbb.com");
            var client = new RestClient("https://imgbb.com");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddFile("", "/C:/Users/hitsa/Desktop/Авы/NFT.png");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }


    }
}
