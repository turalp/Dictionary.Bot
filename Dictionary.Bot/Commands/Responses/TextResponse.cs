using System.Threading.Tasks;
using Dictionary.Bot.Commands.Responses.Abstract;
using Dictionary.Services.Services;
using Telegram.Bot;

namespace Dictionary.Bot.Commands.Responses
{
    public class TextResponse : ICommandResponse
    {
        private readonly string _message;

        public TextResponse(string message)
        {
            _message = message;
        }

        public async Task SendAsync(ITelegramBotClient bot, long chatId)
        {
            await BotService.Send(bot, chatId, _message);
        }
    }
}
