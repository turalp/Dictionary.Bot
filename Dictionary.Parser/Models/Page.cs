using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Domain.Models;
using Dictionary.Parser.Helpers;
using Dictionary.Parser.Models.Abstract;
using HtmlAgilityPack;

namespace Dictionary.Parser.Models
{
    /// <summary>
    /// Provides implementation methods to parse dictionary site pages.
    /// </summary>
    internal class Page : IPage
    {
        public Page()
        {
            WordsLinks = new List<string>();
        }

        public bool HasNextPage { get; set; }

        public string Letter { get; set; }

        /// <summary>
        /// Words links that were found in the page.
        /// </summary>
        public IList<string> WordsLinks { get; set; }

        public void Parse(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page));
            }
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(page);

            HtmlNodeCollection links = document
                .DocumentNode
                .SelectNodes("(//div[contains(@style, 'margin-bottom:1em')]//a[1])");

            if (!WordsLinks.Contains(links[0].Attributes["href"].Value))
            {
                WordsLinks.Add(links);

                HasNextPage = true;
            }
            else
            {
                HasNextPage = false;
                HtmlNodeCollection letterLinks = document
                    .DocumentNode
                    .SelectNodes("(//a[contains(@class, 'dict letter big')])");
                int index = letterLinks.GetNodeIndex(letterLinks.FirstOrDefault(l => l.HasClass("active")));
                Letter = index != letterLinks.Count - 1 ? letterLinks[index + 1].InnerText : null;
            }
        }

        public KeyValuePair<Word, Description> ParseWord(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page));
            }

            Word word = new Word();
            Description description = new Description();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(page);

            HtmlNode htmlWordNode = document
                .DocumentNode
                .SelectSingleNode("(//div[contains(@itemprop, 'articleBody')]//h1[1])");
            word.Title = htmlWordNode.InnerText;

            HtmlNode htmlDescriptionNode = document
                .DocumentNode
                .SelectSingleNode("(//div[contains(@itemprop, 'articleBody')])");
            description.Content = htmlDescriptionNode.InnerText
                .Replace("\r\n", "")
                .Replace("  ", "")
                .Substring(htmlWordNode.InnerText.Length + 1);

            return new KeyValuePair<Word, Description>(word, description);
        }
    }
}
