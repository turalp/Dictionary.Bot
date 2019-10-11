using System.Collections.Generic;
using Dictionary.Domain.Models;

namespace Dictionary.Parser.Models.Abstract
{
    internal interface IPage
    {
        bool HasNextPage { get; set; }

        string Letter { get; set; }

        IList<string> WordsLinks { get; set; }

        void Parse(string page);

        KeyValuePair<Word, Description> ParseWord(string page);
    }
}
