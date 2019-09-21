using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Dictionary.Parser.Helpers
{
    public static class ListExtensions
    {
        public static void Add(this ICollection<string> list, ICollection<HtmlNode> collection)
        {
            if (!IsValidCollection(list) && !IsValidCollection(collection))
            {
                throw new ArgumentException("Collections are not valid.");
            }

            foreach (HtmlNode node in collection)
            {
                list.Add(node.ToString());
            }
        }

        private static bool IsValidCollection<T>(ICollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
