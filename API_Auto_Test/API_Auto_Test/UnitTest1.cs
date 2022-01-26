using RestSharp;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Threading;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Html5;

namespace API_Auto_Test
{
    public class UnitTEst1
    {
        [Fact]
        public void Test1()
        {
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                { "ulogin", "art1613122" },
                { "upassword", "505558545" }
            };
            var response = API_Helper.SendJSON_API_Request("https://my.soyuz.in.ua/index.php", "Content-Type", "application/x-www-form-urlencoded", body);

            var cookie = API_Helper.ExtractCookie(response);

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");

            foreach (Cookie cookies in API_Helper.ExtractCookie(response))
                driver.Manage().Cookies.AddCookie(cookies);

            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            Thread.Sleep(15000);
        }
    }
}
