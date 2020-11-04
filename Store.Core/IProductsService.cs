using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.Queries;
using Store.DAL.Models;

namespace Store.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<Category>> GetCategoriesWithProducts();
        Task<Product> GetProductById(int id);
        Task<PagedResult<Product>> GetProducts(ProductQuery query);
        IQueryable<Product> GetProductsFilteredByPrice(decimal price);
        Task<IEnumerable<Category>> GetCategories();
    }
}