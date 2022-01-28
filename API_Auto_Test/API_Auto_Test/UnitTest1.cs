using RestSharp;
using System.Collections.Generic;
using Xunit;
using System.IO;
using System.Text.Json;
using System;

namespace API_Auto_Test
{
    public class UnitTEst1
    {
        [Fact]
        public void Test_Upload_picture()
        {
            var body = new Dictionary<string, string>
            {
                { "content", "Users/hitsa/Desktop/Avatars/test3.jpg"}
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data"},
            };

            var response = API_Helper.SendJSON_API_Request(body, headers, "https://imgbb.com", Method.POST);
            Assert.Equal("OK", response.StatusCode.ToString());
        }
        [Fact]
        public void Test_Download_picture()
        {
            byte[] content = API_Helper.SendJsonApiRequest("https://amnesiacky.github.io/images/2663068.png");
            File.WriteAllBytes(Path.Combine("/Users/hitsa/Desktop/Avatars", "test3.jpg"), content);
        }
        [Fact]
        public void Test_TakeAndAdd_Token()
        {
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "authority", "api.newbookmodels.com" }
            };
            var body = new Dictionary<string, string>
            {
                { "password", "12345678yariK!" },
                { "email", "gustavfergusson@gmail.com" }
            };

            //Send API Login
            var response = API_Helper.SendJSON_API_Request(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/", Method.POST);

            string jsonString =JsonSerializer.Serialize<string>(response.Content.ToString());
            
            var headers2 = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "token_data", jsonString }
            };
            var body2 = new Dictionary<string, string>
            {
                { "1", "" },
                { "2", "" }
            };

            var response1 = API_Helper.SendJSON_API_Request(body, headers2, "https://newbookmodels.com/auth/signin?goBackUrl=%2Fexplore", Method.POST);

            Assert.Equal("OK", response1.StatusCode.ToString());
        }
        [Fact]
        public void Test_CheckHandler()
        {
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "authority", "api.newbookmodels.com" }
            };
            var body = new Dictionary<string, string>
            {
                { "password", "12345678yariK!" },
                { "email", "gustavfergusson@gmail.com" }
            };

            //Send API Login
            var response = API_Helper.SendJSON_API_Request(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/", Method.POST);

            ResponseHandler handler = new ResponseHandler(response);
            Console.WriteLine(handler.headers);
            Console.WriteLine(handler.content);
            Console.WriteLine(handler.statusCode);
            Console.WriteLine(handler.contentLength);
            Console.WriteLine(handler.contentType);
        }


        //[Fact]
        //public void Test1()
        //{
        //    //Set data for API request
        //    var body = new Dictionary<string, string>
        //    {
        //        { "ulogin", "art1613122" },
        //        { "upassword", "505558545" }
        //    };
        //    var headers = new Dictionary<string, string>
        //    {
        //        { "Content-Type", "application/x-www-form-urlencoded" },
        //    };

        //    //Send API Login
        //    var response = API_Helper.SendJSON_API_Request(body, headers, "https://my.soyuz.in.ua/", Method.POST);

        //    //Get Cookie  & Change Cookie 
        //    var cookie = API_Helper.ExtractCookie(response, "zbs_lang");
        //    var cookie2 = API_Helper.ExtractCookie(response, "ulogin");
        //    var cookie3 = API_Helper.ExtractCookie(response, "upassword");

        //    //Open browser
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Navigate().GoToUrl("https://my.soyuz.in.ua");
        //    driver.Manage().Window.Maximize();

        //    //Set cookie to browser
        //    driver.Manage().Cookies.AddCookie(cookie);
        //    driver.Manage().Cookies.AddCookie(cookie2);
        //    driver.Manage().Cookies.AddCookie(cookie3);


        //    //Open site for check
        //    driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

        //    Thread.Sleep(5000);
        //    driver.Close();
        //}
    }
}
