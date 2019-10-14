﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Domain.Base;
using Dictionary.Domain.Constants;
using Dictionary.Domain.Repositories;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Services.Services;
using Dictionary.Services.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Dictionary.Bot
{
    internal class Program
    {
        private static ITelegramBotClient _bot;
        private static ICommandManager _manager;
        private static IDictionaryService _dictionaryService;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting bot...");
            ConfigureServices();
            _bot.OnMessage += OnMessageReceived;
            Console.WriteLine("Start to receive messages...");
            _bot.StartReceiving();

            Task.Delay(-1).Wait();
            _bot.StopReceiving();
            Console.WriteLine("Stop to receive messages...");
        }

        private static async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
            {
                Console.WriteLine("Message type is not relevant for processing.");
                return;
            }
            
            string[] messageParts = message.Text.Split(' ');
            string word;
            long chatId = message.Chat.Id;
            int messagePartsLength = messageParts.Length;
            
            if (messagePartsLength > 2)
            {
                Console.WriteLine("Message has too much words to explain. Please, send one word to get explanation.");
                return;
            }
            
            if (messagePartsLength == 2)
            {
                if (!messageParts[0].StartsWith('/'))
                {
                    Console.WriteLine("Message has too much words to explain. Please, send one word to get explanation.");
                    return;
                }

                word = messageParts[1];

                await _manager.Process(chatId, word, messageParts[0], _dictionaryService);
            }
            else
            {
                if (BotCommands.Explain.Contains(messageParts[0]) || BotCommands.Help.Contains(messageParts[0]))
                {
                    await _manager.Process(chatId, null, messageParts[0], _dictionaryService);
                }
                else
                {
                    word = messageParts[0];
                    await _manager.Process(chatId, word, dictionaryService: _dictionaryService);
                }
            }
        }

        private static void ConfigureServices()
        {
            var context = new DictionaryContext();
            context.Database.Migrate();

            IUnitOfWork unitOfWork = new UnitOfWork(context);
            _dictionaryService = new DictionaryService(unitOfWork);
            _bot = BotService.GetBot();
            _manager = new CommandManager(_bot);
        }
    }
}
