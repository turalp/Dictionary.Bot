using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories.Abstract;
using Dictionary.Services.Services;
using Dictionary.Services.Services.Abstract;
using Moq;
using NUnit.Framework;

namespace Dictionary.Tests
{
    [TestFixture]
    public class DictionaryServiceTests
    {
        private IDictionaryService  _dictionaryService;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _dictionaryService = new DictionaryService(_unitOfWork.Object);
        }

        [Test]
        public void GetClosestWords_CheckWordPresence()
        {
            var words = new List<Word>
            {
                new Word { Title = "KILL" },
                new Word { Title = "PILL" },
                new Word { Title = "WORD" },
                new Word { Title = "WILL" },
                new Word { Title = "MISS" },
                new Word { Title = "MISSED" },
                new Word { Title = "PERMIT" },
                new Word { Title = "PERMITE" }
            };

            _unitOfWork
                .Setup(w => w.GetRepository<Word>().GetAll())
                .Returns(words.AsQueryable);
            
            // Act
            var resultWords = _dictionaryService.GetClosestWords("kill");
            
            // Assert
            Assert.That(resultWords, Has.Member(words[0])); // KILL
            Assert.That(resultWords, Has.Member(words[1])); // PILL
            Assert.That(resultWords, Has.Member(words[3])); // WILL
            Assert.That(resultWords.Length, Is.EqualTo(3));
        }
        
        [Test]
        [TestCase("KISS", 1)]
        [TestCase("Permite", 2)]
        [TestCase("SKiLL", 1)]
        [TestCase("LORD", 1)]
        [TestCase("KILL", 3)]
        public void GetClosestWords_NormalBehaviour(string word, int count)
        {
            // Arrange
            var words = new List<Word>
            {
                new Word { Title = "KILL" },
                new Word { Title = "PILL" },
                new Word { Title = "WORD" },
                new Word { Title = "WILL" },
                new Word { Title = "MISS" },
                new Word { Title = "MISSED" },
                new Word { Title = "PERMIT" },
                new Word { Title = "PERMITE" }
            };

            _unitOfWork
                .Setup(w => w.GetRepository<Word>().GetAll())
                .Returns(words.AsQueryable);
            
            // Act
            var resultWords = _dictionaryService.GetClosestWords(word);
            
            // Assert
            Assert.That(resultWords.Length, Is.EqualTo(count));
        }

        [Test]
        public void GetClosestWords_ShouldThrowException()
        {
            // Act & Assert
            Assert.That(
                () => _dictionaryService.GetClosestWords(""), 
                Throws.TypeOf<ArgumentNullException>());
        }
    }
}