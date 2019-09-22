using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories;

namespace Dictionary.Services.Services
{
    public class DictionaryService
    {
        public async Task InsertWordsAsync(ICollection<Word> words)
        {
            using (var repository = new DictionaryRepository())
            {
                words = NormalizeWords(words);
                foreach (Word word in words)
                {
                    await repository.InsertAsync(word);
                }
            }
        }

        private ICollection<Word> NormalizeWords(ICollection<Word> words)
        {
            return words
                .Select(w =>
                {
                    if (Regex.IsMatch(w.Title, @"\d"))
                    {
                        w.Title = w.Title.Substring(1);
                    }

                    return w;
                }).ToArray();
        }
    }
}
