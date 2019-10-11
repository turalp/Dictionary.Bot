using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Domain.Models;

namespace Dictionary.Services.Services.Abstract
{
    public interface IDictionaryService
    {
        Task InsertWordsAsync(IDictionary<string, Description[]> words);

        Task<Word> GetByWordAsync(string word);
    }
}
