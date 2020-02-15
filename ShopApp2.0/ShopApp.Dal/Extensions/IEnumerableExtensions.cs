using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopApp.Dal
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null || collection?.Count() == 0;

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> collection, string property)
            where T : new()
        {
            return collection.OrderBy(x => x.GetType().GetProperty(property)?.GetValue(x, null));
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> collection, string property)
            where T : new()
        {
            return collection.OrderByDescending(x => x.GetType().GetProperty(property)?.GetValue(x, null));
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> collection, Func<T, object> textFunc,
            Func<T, object> valueFunc)
        {
            return collection.Select(t => new SelectListItem
            {
                Text = textFunc(t).ToString(),
                Value = valueFunc(t).ToString()
            }).ToList();
        }
    }
}