using System;
using System.Configuration;
using System.Net.Http;
using Dictionary.Parser.Models;
using Dictionary.Parser.Models.Abstract;

namespace Dictionary.Parser
{
    internal class Program
    {
        private static IPage _page;
        private static HttpClient _httpClient;

        internal static void Main(string[] args)
        {
            _page = new Page();

            using (_httpClient = new HttpClient())
            {
                string response = _httpClient
                    .GetStringAsync(ConfigurationManager.AppSettings["InitialUrl"])
                    .Result;
                _page.Parse(response);

                while (_page.NextPageLink != null)
                {

                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
