using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DAL;
using Store.DAL.Models;

namespace Store.App.Core
{
    public class ProductsService : IProductsService
    {
        private readonly StoreContext context;


        public ProductsService(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            var categories = context.Categories.Where(c => c.Products.Any());

            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}