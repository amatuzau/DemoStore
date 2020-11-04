using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Queries;
using Store.DAL;
using Store.DAL.Models;

namespace Store.Core
{
    public class ProductsService : IProductsService
    {
        private readonly StoreContext context;
        private readonly ISortingProvider<Product> sortingProvider;

        public ProductsService(StoreContext context, ISortingProvider<Product> sortingProvider)
        {
            this.context = context;
            this.sortingProvider = sortingProvider;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.ToArrayAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            var categories = context.Categories.Where(c => c.Products.Any());

            return await categories.ToListAsync();
        }

        public async Task<PagedResult<Product>> GetProducts(ProductQuery query)
        {
            var queryable = context.Products.AsQueryable();

            if (query.Categories != null)
            {
                queryable = queryable.Where(p => query.Categories.Contains(p.CategoryId));
            }

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);
            queryable = queryable.ApplyPagination(query);
            
            var items = await queryable.ToListAsync();

            return new PagedResult<Product>{TotalCount = count, Items = items};
        }

        public IQueryable<Product> GetProductsFilteredByPrice(decimal price)
        {
            var products = context.Products.Where(p => p.Price > price);

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}