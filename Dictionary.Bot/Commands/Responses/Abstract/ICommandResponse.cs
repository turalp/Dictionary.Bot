using System.Threading.Tasks;
using Telegram.Bot;

namespace Dictionary.Bot.Commands.Responses.Abstract
{
    public interface ICommandResponse
    {
        Task SendAsync(ITelegramBotClient bot, long chatId);
    }
}
