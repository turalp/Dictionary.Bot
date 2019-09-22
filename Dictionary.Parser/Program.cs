using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories;
using Dictionary.Parser.Helpers;
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
            Console.WriteLine("STARTING Parser...");

            _page = new Page();
            string domain = ConfigurationManager.AppSettings["Domain"];
            ICollection<string> links = new List<string>();

            using (_httpClient = new HttpClient())
            {
                string response = _httpClient
                    .GetStringAsync(ConfigurationManager.AppSettings["InitialUrl"])
                    .Result;
                _page.Parse(response);

                string pageMarkup;
                while (_page.NextLetterPageLink != null)
                {
                    while (_page.NextPageLink != null)
                    {
                        links.Add(_page.WordsLinks);

                        pageMarkup = _httpClient
                            .GetStringAsync(domain + _page.NextPageLink)
                            .Result;
                        _page.Parse(pageMarkup);
                    }
                }

                foreach (string link in links)
                {
                    using (var repository = new DictionaryRepository())
                    {
                        pageMarkup = _httpClient
                            .GetStringAsync(domain + link)
                            .Result;
                        Word word = _page.ParseWord(pageMarkup);
                        repository.InsertAsync(word).GetAwaiter().GetResult();
                    }
                }

                Console.WriteLine("Parser was FINISHED successfully.");
                Console.ReadKey();
            }
        }
    }
}
