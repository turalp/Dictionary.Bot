using System;
using System.Collections.Generic;
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
                Guid wordId = _unitOfWork.GetRepository<Word>().Insert(key);

                IRepository<Description> descriptionRepository = _unitOfWork.GetRepository<Description>();
                foreach (Description description in word.Value)
                {
                    description.WordId = wordId;
                    descriptionRepository.Insert(description);
                }
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
