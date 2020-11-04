using System.Linq;

namespace Store.Core.Queries
{
    public interface ISortingProvider<T>
    {
        IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query);
    }
}