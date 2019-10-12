using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories.Abstract;
using IDictionaryService = Dictionary.Services.Services.Abstract.IDictionaryService;

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
            foreach (KeyValuePair<string, Description[]> word in words)
            {
                Word key = new Word { Title = word.Key };
                Guid wordId = await _unitOfWork.GetRepository<Word>().InsertOrUpdateAsync(key);

                IRepository<Description> descriptionRepository = _unitOfWork.GetRepository<Description>();
                foreach (Description description in word.Value)
                {
                    description.WordId = wordId;
                    await descriptionRepository.InsertOrUpdateAsync(description);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<Word> GetByWordAsync(string word)
        {
            IRepository<Word> wordRepository = _unitOfWork.GetRepository<Word>();
            Word result = await wordRepository.GetSingleAsync(w => w.Title == word);

            if (result == null)
            {
                return null;
            }

            IQueryable<Description> descriptions = _unitOfWork
                .GetRepository<Description>()
                .GetByCondition(d => d.WordId == result.Id);
            foreach (Description description in descriptions)
            {
                result.Descriptions.Add(description);
            }

            return result;
        }

        public Word[] GetClosestWords(string word)
        {
            IQueryable<Word> allWords = _unitOfWork.GetRepository<Word>().GetAll();
            List<Word> result = new List<Word>();
            foreach (Word dbWord in allWords)
            {
                int distance = CalculateEditDistance(word, dbWord.Title, word.Length, dbWord.Title.Length);
                if (distance < 3)
                {
                    result.Add(dbWord);
                }
            }

            return result.ToArray();
        }

        private int CalculateEditDistance(string firstWord, string lastWord, int firstWordIndex, int lastWordIndex)
        {
            int[,] distances = new int[firstWordIndex,lastWordIndex];

            for (int i = 0; i < firstWordIndex; i++)
            {
                for (int j = 0; j < lastWordIndex; j++)
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
                        distances[i, j] = 1 + Math.Min(distances[i, j - 1], Math.Min(
                                              distances[i - 1, j], 
                                              distances[i - 1, j - 1]));
                    }
                }
            }

            return distances[firstWordIndex, lastWordIndex];
        }
    }
}
