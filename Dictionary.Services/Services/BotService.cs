using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

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
                if (message.Length >= 4096)
                {
                    string[] messages = DivideLongMessage(message);
                    foreach (string messagePart in messages)
                    {
                        await bot.SendTextMessageAsync(
                            chatId,
                            messagePart,
                            ParseMode.Html,
                            true
                        );
                    }
                }
                else
                {
                    await bot.SendTextMessageAsync(
                        chatId,
                        message,
                        ParseMode.Html,
                        true
                    );
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed sending message with message: {exception.Message}");
                await bot.SendTextMessageAsync(chatId, "Təəssüf ki, sözün izahı tapılmadı.", ParseMode.Html);
            }
        }

        private static string[] DivideLongMessage(string message)
        {
            List<string> result = new List<string>();
            int messageCount = message.Length / 4096;

            string[] messageParts = message.Split('.');
            StringBuilder messagePart = new StringBuilder();
            for (int i = 0; i < messageCount; i++)
            {
                foreach (string sentence in messageParts)
                {
                    if (messagePart.Length > 3000)
                    {
                        result.Add(messagePart + ".");
                        messagePart.Clear();
                    }

                    messagePart.Append(sentence);
                    messagePart.Append(".");
                }
            }

            return result.ToArray();
        }
    }
}
