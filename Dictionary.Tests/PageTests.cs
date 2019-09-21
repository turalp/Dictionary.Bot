using System;
using System.IO;
using Dictionary.Parser.Models;
using Dictionary.Parser.Models.Abstract;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Dictionary.Tests
{
    [TestFixture]
    public class PageTests
    {
        private readonly IPage _page = new Page();
        private string _htmlMarkup;

        [SetUp]
        public void SetUp()
        {
            _htmlMarkup = File.ReadAllText(
                Path.Combine(
                    @"C:\Users\Tural\source\repos\Dictionary.Bot\Dictionary.Tests", "Files", "page.html"));
        }

        [Test]
        public void Parse()
        {
            // Act
            _page.Parse(_htmlMarkup);

            // Assert
            Assert.That(_page.WordsLinks, Has.Count.GreaterThan(0));
            Assert.That(_page.NextPageLink, Is.Not.Null);
            Assert.That(_page.NextLetterPageLink, Is.Not.Null);
        }

        [Test]
        public void Parse_WhenParameterNullOrEmpty_ShouldThrowException()
        {
            Assert.That(() => _page.Parse(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => _page.Parse(""), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
