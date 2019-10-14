using System;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task<ICommandResponse> ExecuteAsync(long chatId, string args, IDictionaryService dictionaryService)
        {
            if (string.IsNullOrEmpty(args))
            {
                throw new ArgumentNullException();
            }

            Word word = await dictionaryService.GetWord(args);
            if (word == null)
            {
                Word[] closestWords = dictionaryService.GetClosestWords(args);
                StringBuilder words = new StringBuilder("Word was not found. Maybe you meant:\n");
                foreach (Word closestWord in closestWords)
                {
                    words.AppendLine("• " + closestWord.Title.ToLower());
                }

                return new TextResponse(words.ToString());
            }

            string descriptions = await dictionaryService.GetDescriptionByWordAsync(word);
            return new TextResponse(descriptions);
        }
    }
}
