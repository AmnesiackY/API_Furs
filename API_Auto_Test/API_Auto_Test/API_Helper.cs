using RestSharp;
using System.Collections.Generic;
using OpenQA.Selenium;
using RestSharp.Serialization.Json;
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
        public static byte[] SendJsonApiRequest(string imageUrl)
        {
            var client = new RestClient(imageUrl);
            var request2 = new RestRequest(Method.GET);
            byte[] imageAsBytes = client.DownloadData(request2);
            return imageAsBytes;
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
    }
}
