using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using Dictionary.Domain.Models;
using Dictionary.Parser.Models;
using Dictionary.Parser.Models.Abstract;
using Dictionary.Services.Services;

namespace Dictionary.Parser
{
    internal class Program
    {
        private static IPage _page;
        private static HttpClient _httpClient;

        internal static void Main(string[] args)
        {
            Console.WriteLine("STARTING Parser...");

            _page = new Page
            {
                Letter = "A"
            };
            string domain = ConfigurationManager.AppSettings["Domain"];
            ICollection<string> links = new List<string>();

            using (_httpClient = new HttpClient())
            {
                string pageMarkup;
                int pageNumber;
                while (_page.Letter != null)
                {
                    Console.WriteLine($"Processing letter {_page.Letter}...");
                    pageNumber = 1;
                    do
                    {
                        Console.WriteLine($"Processing page #{pageNumber}");

                        pageMarkup = _httpClient
                            .GetStringAsync(
                                domain + $"/ru/dict/exp/byletter/{_page.Letter}/?p={pageNumber}")
                            .Result;
                        _page.Parse(pageMarkup);
                        pageNumber++;
                    } while (_page.HasNextPage);
                    Thread.Sleep(2000);
                }

                List<Word> words = new List<Word>();
                foreach (string link in _page.WordsLinks)
                {
                    pageMarkup = _httpClient
                        .GetStringAsync(domain + link)
                        .Result;
                    Word word = _page.ParseWord(pageMarkup);
                    words.Add(word);
                }
                CsvFileService.SaveToFile(words);

                Console.WriteLine($"Parser was FINISHED successfully. {links.Count} words was inserted.");
                Console.ReadKey();
            }
        }
    }
}
