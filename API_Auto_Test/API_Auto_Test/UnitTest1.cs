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
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://httpbin.org/#/Images/get_image_jpeg");
            var client = new RestClient("https://imgbb.com");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddFile("", "/Users/hitsa/Desktop/ôûâ/API_Furs/wf.jpg");
            IRestResponse response = client.Execute(request);
            client.DownloadData(request).SaveAs("/Users/hitsa/Desktop/ôûâ/API_Furs/test.jpg");
        }
        // 
        //
    }
}
