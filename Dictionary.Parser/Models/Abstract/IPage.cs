using System.Collections.Generic;
using Dictionary.Domain.Models;

namespace Dictionary.Parser.Models.Abstract
{
    internal interface IPage
    {
        string NextPageLink { get; set; }

        bool HasNextPage { get; set; }

        string Letter { get; set; }

        string NextLetterPageLink { get; set; }

        IList<string> WordsLinks { get; set; }

        void Parse(string page);

        Word ParseWord(string page);
    }
}
