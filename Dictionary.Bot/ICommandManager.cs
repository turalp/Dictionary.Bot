using System.Threading.Tasks;

namespace Dictionary.Bot
{
    public interface ICommandManager
    {
        Task Process(long chatId, string word, string commandName = null);
    }
}
