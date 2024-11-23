using Boilerplate.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Boilerplate.Application.Extentions
{
    public static class QueryableExtention
    {
        public static IQueryable<TSource> AddOrderBy<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    source = source.OrderBy(orderBy);
                else
                    source = source.OrderByDescending(orderBy);
            }
            return source;
        }

        public static IQueryable<TSource> AddPage<TSource>(this IQueryable<TSource> source, int? skip, int? take)
        {
            if (skip.HasValue)
                source = source.Skip(skip.Value);

            if (take.HasValue)
                source = source.Take(take.Value);
            return source;
        }
    }
}
