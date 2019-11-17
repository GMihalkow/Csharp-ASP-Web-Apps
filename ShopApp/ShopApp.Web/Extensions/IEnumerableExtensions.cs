using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopApp.Web
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> collection, Func<T, object> textFunc, Func<T, object> valueFunc) where T : class
        {
            return collection.Select(e => new SelectListItem { Text = textFunc(e).ToString(), Value = valueFunc.ToString() });
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection?.Count() == 0;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> collection, string property)
            where T : new()
        {
            return collection.OrderBy(x => x.GetType().GetProperty(property).GetValue(x, null));
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> collection, string property)
            where T : new()
        {
            return collection.OrderByDescending(x => x.GetType().GetProperty(property).GetValue(x, null));
        }
    }
}