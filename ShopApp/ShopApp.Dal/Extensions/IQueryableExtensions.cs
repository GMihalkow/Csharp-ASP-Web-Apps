using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ShopApp.Web
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, Expression<Func<T, object>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;
            if (member == null){ throw new ArgumentException("Expression doesn't refer to a property."); }

            return source.Include(member.Member.Name);
        }
    }
}