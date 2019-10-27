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
    }
}