using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EZSubmitApp.Core.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, List<Expression<Func<T, bool>>> filterList)
        {
            if (filterList == null)
                return query;

            foreach(var filter in filterList)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
