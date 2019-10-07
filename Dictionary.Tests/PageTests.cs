using System;
using System.IO;
using Dictionary.Domain.Models;
using Dictionary.Parser.Models;
using Dictionary.Parser.Models.Abstract;
using NUnit.Framework;

namespace Dictionary.Tests
{
    [TestFixture]
    public class PageTests
    {
        private readonly IPage _page = new Page();
        private string _fHtmlMarkup;
        private string _sHtmlMarkup;

        [SetUp]
        public void SetUp()
        {
            _fHtmlMarkup = File.ReadAllText(
                Path.Combine(
                    @"C:\Users\Tural\source\repos\Dictionary.Bot\Dictionary.Tests", "Files", "page3.html"));
            _sHtmlMarkup = File.ReadAllText(
                Path.Combine(
                    @"C:\Users\Tural\source\repos\Dictionary.Bot\Dictionary.Tests", "Files", "page2.html"));
        }

        [Test]
        public void Parse()
        {
            // Act
            _page.Parse(_fHtmlMarkup);

            // Assert
            Assert.That(_page.WordsLinks, Has.Count.GreaterThan(0));
            //Assert.That(_page.NextPageLink, Is.Not.Null);
            //Assert.That(_page.NextLetterPageLink, Is.Not.Null);
        }

        [Test]
        public void Parse_WhenParameterNullOrEmpty_ShouldThrowException()
        {
            Assert.That(() => _page.Parse(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => _page.Parse(""), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void ParseWord()
        {
            //Act
            //Word result = _page.ParseWord(_sHtmlMarkup);

            //Assert.That(result.Title, Is.Not.Null);
            //Assert.That(result.Description, Is.Not.Null);
        }

        [Test]
        public void ParseWord_WhenParameterNullOrEmpty_ShouldThrowException()
        {
            Assert.That(() => _page.ParseWord(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => _page.ParseWord(""), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
