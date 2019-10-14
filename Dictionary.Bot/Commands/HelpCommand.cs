using System.Threading.Tasks;
using Dictionary.Bot.Commands.Abstract;
using Dictionary.Bot.Commands.Responses;
using Dictionary.Bot.Commands.Responses.Abstract;
using Dictionary.Domain.Constants;
using Dictionary.Services.Services.Abstract;

namespace Dictionary.Bot.Commands
{
    public class HelpCommand : ICommand
    {
        public async Task<ICommandResponse> ExecuteAsync(long chatId, string args, IDictionaryService dictionaryService = null)
        {
            return await Task.Run(() => new TextResponse($"{BotSyntax.Help}\r\n" +
                                                         $"{BotSyntax.Explain}\r\n" +
                                                         $"{BotSyntax.SimpleExplain}"));
        }
    }
}
