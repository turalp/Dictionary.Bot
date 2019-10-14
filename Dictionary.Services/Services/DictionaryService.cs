using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Services.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Services.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DictionaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InsertWordsAsync(IDictionary<string, Description[]> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            foreach (KeyValuePair<string, Description[]> wordDescriptionsPair in words)
            {
                Word word = new Word { Title = wordDescriptionsPair.Key };
                Guid wordId = await _unitOfWork.GetRepository<Word>().InsertOrUpdateAsync(word);

                IRepository<Description> descriptionRepository = _unitOfWork.GetRepository<Description>();
                IRepository<FullWord> wordRepository = _unitOfWork.GetRepository<FullWord>();
                foreach (Description description in wordDescriptionsPair.Value)
                {
                    Guid descriptionId = await descriptionRepository.InsertOrUpdateAsync(description);
                    FullWord fullWord = new FullWord
                    {
                        DescriptionId = descriptionId,
                        WordId = wordId,
                    };
                    await wordRepository.InsertOrUpdateAsync(fullWord);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<Word> GetWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            return await _unitOfWork
                .GetRepository<Word>()
                .GetSingleAsync(w => string.Equals(
                    w.Title, 
                    word, 
                    StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Description[]> GetDescriptionByWordAsync(Word word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            IQueryable<FullWord> fullWords = _unitOfWork
                .GetRepository<FullWord>()
                .GetByCondition(w => w.WordId == word.Id);
            IQueryable<Description> descriptions = _unitOfWork
                .GetRepository<Description>()
                .GetByCondition(d => fullWords.Any(w => w.DescriptionId == d.Id));

            Regex regex = new Regex("\\d\\.");
            foreach (Description description in descriptions)
            {
                MatchCollection matches = regex.Matches(description.Content);
                foreach (Match match in matches)
                {
                    if (match.Value != "1.")
                    {
                        description.Content = description.Content
                            .Replace(match.Value, "\n" + match.Value);
                    }
                }
            }

            return await descriptions.ToArrayAsync();
        }

        public Word[] GetClosestWords(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            word = word.ToUpper();
            IQueryable<Word> allWords = _unitOfWork.GetRepository<Word>().GetAll();
            List<Word> result = new List<Word>();
            foreach (Word dbWord in allWords)
            {
                if (dbWord.Title.Length >= word.Length + 1 || dbWord.Title.Length <= word.Length - 1)
                {
                    continue;
                }
                int distance = CalculateEditDistance(
                    word, 
                    dbWord.Title, 
                    word.Length, 
                    dbWord.Title.Length);
                if (distance < 2)
                {
                    result.Add(dbWord);
                }
            }

            return result.ToArray();
        }

        private int CalculateEditDistance(string firstWord, string lastWord, int firstWordLength, int lastWordLength)
        {
            if (firstWordLength == 0)
            {
                return lastWordLength;
            }
            if (lastWordLength == 0)
            {
                return firstWordLength;
            }

            int[,] distances = new int[firstWordLength + 1,lastWordLength + 1];

            for (int i = 0; i <= firstWordLength; i++)
            {
                for (int j = 0; j <= lastWordLength; j++)
                {
                    if (i == 0)
                    {
                        distances[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        distances[i, j] = i;
                    }
                    else if (firstWord[i - 1] == lastWord[j - 1])
                    {
                        distances[i, j] = distances[i - 1, j - 1];
                    }
                    else
                    {
                        distances[i, j] = 1 + Min(distances[i, j - 1], 
                                              distances[i - 1, j], 
                                              distances[i - 1, j - 1]);
                    }
                }
            }

            return distances[firstWordLength, lastWordLength];
        }

        private int Min(int x, int y, int z)
        {
            if (x <= y && x <= z)
            {
                return x;
            }
            if (y <= x && y <= z)
            {
                return y;
            }
            
            return z;
        }
    }
}
