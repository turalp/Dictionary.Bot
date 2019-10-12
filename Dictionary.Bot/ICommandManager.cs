using System.Threading.Tasks;
using Dictionary.Services.Services.Abstract;

namespace Dictionary.Bot
{
    public interface ICommandManager
    {
        Task Process(long chatId, string word, string commandName = null, IDictionaryService dictionaryService = null);
    }
}
