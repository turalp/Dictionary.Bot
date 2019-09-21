using System;
using System.Collections.Generic;
using System.Linq;
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

        public string NextPageLink { get; set; }

        public string NextLetterPageLink { get; set; }

        /// <summary>
        /// Words links that were found in the page.
        /// </summary>
        public ICollection<string> WordsLinks { get; set; }

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
                .SelectNodes("(//div[contains(@style, 'margin-bottom:1em')]//a[0]/@href)");
            WordsLinks.Add(links);

            HtmlNode nextPageLink = document
                .DocumentNode
                .SelectSingleNode("(//div[contains(@style, 'float:right')]//a[0]/@href)");
            NextPageLink = nextPageLink.ToString();

            HtmlNodeCollection letterLinks = document
                .DocumentNode
                .SelectNodes("(//a[contains(@class, 'dict letter big')])");
            int index = letterLinks.GetNodeIndex(letterLinks.FirstOrDefault(l => l.HasClass("active")));
            if (index != letterLinks.Count - 1)
            {
                NextLetterPageLink = letterLinks[index + 1].GetAttributeValue("href", null);
            }
        }
    }
}
