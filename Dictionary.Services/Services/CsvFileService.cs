using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Dictionary.Domain.Models;

namespace Dictionary.Services.Services
{
    public static class CsvFileService
    {
        private static readonly string _path = @"C:\Users\Tural\source\repos\Dictionary.Bot\words-without-line.csv";

        public static void SaveToFile(Dictionary<Word, Description> words)
        {
            using var file = new StreamWriter(_path, true);
            foreach (KeyValuePair<Word, Description> word in words)
            {
                file.WriteLine("\"" + word.Key + "\"" + "," + "\"" + word.Value + "\"");
            }
        }

        public static IDictionary<string, Description[]> ReadFromFile()
        {
            IDictionary<string, ICollection<Description>> words = new Dictionary<string, ICollection<Description>>();
            try
            {
                using var file = new StreamReader(_path);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    MatchCollection matches = Regex.Matches(line, "\"(.*?)\"");
                    string word = matches[0].Value;
                    Description description = new Description
                    {
                        Content = matches[1].Value.Replace("\"", ""),
                    };

                    if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(description.Content))
                    {
                        string normalizedWord = NormalizeWord(word);
                        if (!words.ContainsKey(normalizedWord))
                        {
                            words.Add(normalizedWord, new List<Description>());
                        }
                        words[normalizedWord].Add(description);
                    }
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Program was failed with message: {exception.Message}.");
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine($"Program was failed with message: {exception.Message}.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Program was failed with message: {exception.Message}.");
            }

            return words.ToDictionary(k => k.Key, v => v.Value.ToArray());
        }

        private static string NormalizeWord(string word)
        {
            if (Regex.IsMatch(word, "\\p{No}"))
            {
                word = word.Substring(1, word.Length - 3);
            }

            return word;
        }
    }
}
