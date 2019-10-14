using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Bot.Commands;
using Dictionary.Bot.Commands.Abstract;
using Dictionary.Bot.Commands.Responses.Abstract;
using Dictionary.Domain.Constants;
using Dictionary.Services.Services;
using Dictionary.Services.Services.Abstract;
using Telegram.Bot;

namespace Dictionary.Bot
{
    public class CommandManager : ICommandManager
    {
        private readonly ITelegramBotClient _bot;
        private readonly IDictionary<string[], ICommand> _commands = new Dictionary<string[], ICommand>
        {
            { BotCommands.Help, new HelpCommand() },
            { BotCommands.Explain, new WordCommand() },
        };

        public CommandManager(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task Process(long chatId, string word, string commandName = null, IDictionaryService dictionaryService = null)
        {
            try
            {
                ICommand command = GetCommand(commandName);
                ICommandResponse response = await command.ExecuteAsync(chatId, word, dictionaryService);
                await response.SendAsync(_bot, chatId);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await BotService.Send(_bot, chatId, "Invalid message.");
            }
        }

        private ICommand GetCommand(string commandName)
        {
            if (commandName != null)
            {
                foreach (string[] key in _commands.Keys)
                {
                    if (key.Contains(commandName))
                    {
                        return _commands[key];
                    }
                }
            }

            return new WordCommand();
        }
    }
}
