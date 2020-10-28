using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.DAL.Models;

namespace Store.App.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<Category>> GetCategoriesWithProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
        IQueryable<Product> GetProductsFilteredByPrice(decimal price);
        Task<IEnumerable<Category>> GetCategories();
    }
}