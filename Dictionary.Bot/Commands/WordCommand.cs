using System;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Bot.Commands.Abstract;
using Dictionary.Bot.Commands.Responses;
using Dictionary.Bot.Commands.Responses.Abstract;
using Dictionary.Domain.Models;
using Dictionary.Services.Services.Abstract;

namespace Dictionary.Bot.Commands
{
    public class WordCommand : ICommand
    {
        private readonly IDictionaryService _dictionaryService;

        public WordCommand(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task<ICommandResponse> ExecuteAsync(long chatId, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                throw new ArgumentNullException();
            }

            Word word = await _dictionaryService.GetWordAsync(args);
            if (word == null)
            {
                Word[] closestWords = _dictionaryService.GetClosestWords(args);
                if (closestWords.Length == 0)
                {
                    return new TextResponse(Resources.NoWordMessage);
                }
                StringBuilder words = new StringBuilder(Resources.WordMismatchMessage + "\n");
                foreach (Word closestWord in closestWords)
                {
                    words.AppendLine("• " + closestWord.Title.ToLower());
                }

                return new TextResponse(words.ToString());
            }

            string descriptions = await _dictionaryService.GetDescriptionByWordAsync(word);
            return new TextResponse(descriptions);
        }
    }
}
