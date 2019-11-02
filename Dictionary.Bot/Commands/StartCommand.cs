using System.Threading.Tasks;
using Dictionary.Bot.Commands.Abstract;
using Dictionary.Bot.Commands.Responses;
using Dictionary.Bot.Commands.Responses.Abstract;

namespace Dictionary.Bot.Commands
{
    public class StartCommand : ICommand
    {
        public async Task<ICommandResponse> ExecuteAsync(long chatId, string args)
        {
            return await Task.Run(() => new TextResponse(Resources.StartMessage));
        }
    }
}
