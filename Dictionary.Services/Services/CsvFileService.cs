using System.Collections.Generic;
using System.IO;
using Dictionary.Domain.Models;

namespace Dictionary.Services.Services
{
    public static class CsvFileService
    {
        private static string _path = 
            Path.Combine(@"C:\Users\Tural\source\repos\Dictionary.Bot\", "words.csv");

        public static void SaveToFile(Word[] words)
        {
            using (var file = new StreamWriter(_path, true))
            {
                foreach (Word word in words)
                {
                    file.WriteLine(word.ToString());
                }
            }
        }

        public static Word[] ReadFromFile()
        {
            List<Word> words = new List<Word>();
            using (var file = new StreamReader(_path))
            {
                while (file.ReadLine() != null)
                {
                    string[] result = file.ReadLine()?.Split(',');
                    Word word = new Word();
                    if (result != null)
                    {
                        word.Title = result[0].Trim();
                        word.Description = result[1].Trim();
                        words.Add(word);
                    }
                }
            }

            return words.ToArray();
        }
    }
}
