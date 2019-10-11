using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Services.Services.Abstract;

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
            Word result = await _unitOfWork.GetRepository<Word>().GetSingleAsync(w => w.Title == word);
            
            IQueryable<Description> descriptions =
                _unitOfWork.GetRepository<Description>().GetByCondition(d => d.WordId == result.Id);
            foreach (Description description in descriptions)
            {
                result.Descriptions.Add(description);
            }

            return result;
        }
    }
}
