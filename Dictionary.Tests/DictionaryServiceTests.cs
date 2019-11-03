using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories;
using Dictionary.Services.Services;
using Dictionary.Services.Services.Abstract;
using Dictionary.Tests.Mocks;
using Moq;
using NUnit.Framework;

namespace Dictionary.Tests
{
    [TestFixture]
    public class DictionaryServiceTests
    {
        private readonly Mock<UnitOfWork> _unitOfWork = new Mock<UnitOfWork>();
        private readonly MockRepository<Word> _wordRepository = new MockRepository<Word>();
        private readonly MockRepository<Description> _descriptionRepository = new MockRepository<Description>();
        private readonly MockRepository<FullWord> _fullWordRepository = new MockRepository<FullWord>();

        [SetUp]
        public void SetUpService()
        {
            _unitOfWork
                .Setup(e => e.GetRepository<Word>())
                .Returns(_wordRepository);
            _unitOfWork
                .Setup(e => e.GetRepository<Description>())
                .Returns(_descriptionRepository);
            _unitOfWork
                .Setup(e => e.GetRepository<FullWord>())
                .Returns(_fullWordRepository);
        }

        [Test]
        public async Task InsertWordsAsync_ShouldAddWordWithDescription()
        {
            // Arrange
            IDictionaryService dictionaryService = new DictionaryService(_unitOfWork.Object);
            var description1 = new Description
            {
                Content = "aa"
            };
            var description2 = new Description
            {
                Content = "ab"
            };
            IDictionary<string, Description[]> words = new Dictionary<string, Description[]>
            {
                { "a", new []{ description1, description2, } }
            };

            // Act
            await dictionaryService.InsertWordsAsync(words);

            // Assert
            Assert.That(_wordRepository.Entities, Has.Count.EqualTo(1));
            Assert.That(_wordRepository.Entities[0].Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(_wordRepository.Entities[0].Title, Is.EqualTo("a"));
            Assert.That(_wordRepository.Entities[0].WordDescriptions, Has.Count.EqualTo(2));

            Assert.That(_descriptionRepository.Entities, Has.Count.EqualTo(2));
            Assert.That(_descriptionRepository.Entities, Has.Member(description1));
            Assert.That(_descriptionRepository.Entities, Has.Member(description2));
        }
    }
}
