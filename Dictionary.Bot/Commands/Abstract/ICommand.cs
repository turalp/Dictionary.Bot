using System.Threading.Tasks;
using Dictionary.Bot.Commands.Responses.Abstract;

namespace Dictionary.Bot.Commands.Abstract
{
    public interface ICommand
    {
        Task<ICommandResponse> ExecuteAsync(long chatId, string args);
    }
}
