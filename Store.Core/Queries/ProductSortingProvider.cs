using System;
using System.Linq.Expressions;
using Store.DAL.Models;

namespace Store.Core.Queries
{
    public class ProductSortingProvider: BaseSortingProvider<Product>
    {
        protected override Expression<Func<Product, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Name" => p => p.Name,
                "Price" => p => p.Price,
                _ => p => p.Id
            };
        }
    }
}