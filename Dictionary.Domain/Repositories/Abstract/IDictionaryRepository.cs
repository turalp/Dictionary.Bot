using System.Threading.Tasks;
using Dictionary.Domain.Models;

namespace Dictionary.Domain.Repositories.Abstract
{
    public interface IDictionaryRepository
    {
        Task<Word> GetByWordAsync(string word);

        Task InsertAsync(Word word);

        Task UpdateAsync(Word word);
    }
}
