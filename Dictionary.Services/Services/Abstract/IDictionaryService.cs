using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Domain.Models;

namespace Dictionary.Services.Services.Abstract
{
    public interface IDictionaryService
    {
        Task InsertWordsAsync(IDictionary<string, Description[]> words);

        Task<Word> GetWord(string word);

        Task<string> GetDescriptionByWordAsync(Word word);

        Word[] GetClosestWords(string word);
    }
}
