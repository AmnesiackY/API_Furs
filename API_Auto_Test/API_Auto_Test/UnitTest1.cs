using RestSharp;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Threading;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Html5;
using RestSharp.Extensions;
using System.IO;

namespace API_Auto_Test
{
    public class UnitTEst1
    {
        [Fact]
        public void Test1()
        {
            //Set data for API request
            var body = new Dictionary<string, string>
            {
                { "ulogin", "art1613122" },
                { "upassword", "505558545" }
            };
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" },
            };

            //Send API Login
            var response = API_Helper.SendJSON_API_Request(body,headers,"https://my.soyuz.in.ua/", Method.POST);

            //Get Cookie  & Change Cookie 
            var cookie = API_Helper.ExtractCookie(response, "zbs_lang");
            var cookie2 = API_Helper.ExtractCookie(response, "ulogin");
            var cookie3 = API_Helper.ExtractCookie(response, "upassword");

            //Open browser
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");
            driver.Manage().Window.Maximize();

            //Set cookie to browser
            driver.Manage().Cookies.AddCookie(cookie);
            driver.Manage().Cookies.AddCookie(cookie2);
            driver.Manage().Cookies.AddCookie(cookie3);

            //Open site for check
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            Thread.Sleep(5000);
            driver.Close();
        }
        [Fact]
        public void Test2()
        {
            //var client = new RestClient("https://imgbb.com");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AddFile("content", "/Users/User/Documents/GitArchives/test.jpg");
            //IRestResponse response = client.Execute(request);
            //Assert.Equal("OK", response.StatusCode.ToString());
            var body = new Dictionary<string, string>
            {
                { "content", "/Users/User/Documents/GitArchives/test.jpg"}
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data"}
            };

            var response = API_Helper.SendJSON_API_Request(body, headers, "https://imgbb.com", Method.POST);
            Assert.Equal("OK", response.StatusCode.ToString());
        }
        [Fact]
        public void Test3()
        {
            byte[] content = API_Helper.SendJsonApiRequest("https://amnesiacky.github.io/images/2663068.png");
            File.WriteAllBytes(Path.Combine("/Users/hitsa/Desktop/ôûâ/API_Furs", "test3.jpg"), content);
        }
    }
}
