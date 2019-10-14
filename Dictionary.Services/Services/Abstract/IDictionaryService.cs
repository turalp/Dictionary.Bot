using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using User = Telegram.Bot.Types.User;

namespace Dictionary.Services.Services.Abstract
{
    public interface IDictionaryService
    {
        Task InsertWordsAsync(IDictionary<string, Description[]> words);

        Task<Guid> InsertUser(User user);

        Task<Word> GetWord(string word);

        Task<string> GetDescriptionByWordAsync(Word word);

        Word[] GetClosestWords(string word);
    }
}
