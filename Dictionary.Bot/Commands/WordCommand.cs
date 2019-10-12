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
        public async Task<ICommandResponse> ExecuteAsync(long chatId, string args, IDictionaryService dictionaryService)
        {
            Word word = await dictionaryService.GetByWordAsync(args);
            if (word == null)
            {
                Word[] closestWords = dictionaryService.GetClosestWords(args);
                StringBuilder words = new StringBuilder("Word was not found. Maybe you meant:");
                foreach (Word closestWord in closestWords)
                {
                    words.AppendLine("/" + closestWord.Title);
                }

                return new TextResponse(words.ToString());
            }

            StringBuilder textDescription = new StringBuilder();
            foreach (Description description in word.Descriptions)
            {
                textDescription.AppendLine(description.Content);
            }

            return new TextResponse(textDescription.ToString());
        }
    }
}
