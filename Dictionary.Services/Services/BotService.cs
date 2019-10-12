using System;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dictionary.Services.Services
{
    public class BotService
    {
        public static ITelegramBotClient GetBot()
        {
            string token = ConfigurationManager.ConnectionStrings["BotToken"].ConnectionString;
            var bot = new TelegramBotClient(token);
            
            return bot;
        }

        public static async Task Send(ITelegramBotClient bot, long chatId, string message)
        {
            try
            {
                await bot.SendTextMessageAsync(
                    chatId, 
                    message, 
                    ParseMode.Html, 
                    replyMarkup: new ReplyKeyboardRemove());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed sending message with message: {exception.Message}");
            }
        }
    }
}
