using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.Net.Http;

namespace ShopApp.Api
{
    public class Program
    {
        static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];
            string baseAddress = $"http://localhost:{port}/";

            //Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                
                Console.WriteLine($"Listening on port: {port}");
                Console.ReadLine();
            }
        }
    }
}