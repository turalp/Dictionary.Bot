using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using Dictionary.Domain.Base;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Parser.Models;
using Dictionary.Parser.Models.Abstract;
using Dictionary.Services.Services;
using Dictionary.Services.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Parser
{
    internal class Program
    {
        private static IPage _page;
        private static HttpClient _httpClient;
        private static IDictionaryService _dictionaryService;

        internal static void Main(string[] args)
        {
            Console.WriteLine("STARTING Parser...");
            ConfigureServices();

            if (args.Length == 0)
            {
                Console.WriteLine("Provide one of the following arguments: Parse|Insert");

                string argument = Console.ReadLine();
                Execute(argument);
            }
            else
            {
                Execute(args[0]);
            }

            Console.WriteLine("Tool was FINISHED successfully.");
            Console.ReadKey();
        }

        private static void Execute(string argument)
        {
            if (argument == "Parse")
            {
                _page = new Page
                {
                    Letter = "A"
                };
                string domain = ConfigurationManager.AppSettings["Domain"];

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

                    Dictionary<Word, Description> words = new Dictionary<Word, Description>();
                    foreach (string link in _page.WordsLinks)
                    {
                        pageMarkup = _httpClient
                            .GetStringAsync(domain + link)
                            .Result;
                        KeyValuePair<Word, Description> word = _page.ParseWord(pageMarkup);
                        words.Add(word.Key, word.Value);
                    }
                    CsvFileService.SaveToFile(words);
                }
            }
            else if (argument == "Insert")
            {
                IDictionary<string, Description[]> words = CsvFileService.ReadFromFile();
                _dictionaryService.InsertWordsAsync(words).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("Provide one of the following arguments: Parse|Normalize");

                argument = Console.ReadLine();
                Execute(argument);
            }
        }

        private static void ConfigureServices()
        {
            var context = new DictionaryContext();
            context.Database.Migrate();

            IUnitOfWork unitOfWork = new UnitOfWork(context);
            _dictionaryService = new DictionaryService(unitOfWork);
        }
    }
}
