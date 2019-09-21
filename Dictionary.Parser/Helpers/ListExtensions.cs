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
                list.Add(node.Attributes["href"].Value);
            }
        }

        public static void Add<T>(this ICollection<T> list, ICollection<T> collection)
        {
            if (!IsValidCollection(list) && !IsValidCollection(collection))
            {
                throw new ArgumentException("Collections are not valid.");
            }

            foreach (T item in collection)
            {
                list.Add(item);
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
