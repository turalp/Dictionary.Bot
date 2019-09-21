using System.Collections.Generic;

namespace Dictionary.Parser.Models.Abstract
{
    internal interface IPage
    {
        string NextPageLink { get; set; }

        string NextLetterPageLink { get; set; }

        ICollection<string> WordsLinks { get; set; }

        void Parse(string page);
    }
}
