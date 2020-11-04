using System;
using System.Linq;
using System.Linq.Expressions;

namespace Store.Core.Queries
{
    public abstract class BaseSortingProvider<T> : ISortingProvider<T>
    {
        public IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query)
        {
            var sortExpression = GetSortExpression(query);

            return query.SortOrder switch
            {
                SortOrder.Descending => queryable.OrderByDescending(sortExpression),
                _ => queryable.OrderBy(sortExpression)
            };
        }

        protected abstract Expression<Func<T, object>> GetSortExpression(BaseQuery query);
    }
}