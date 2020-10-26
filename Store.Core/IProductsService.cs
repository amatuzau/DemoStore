using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DAL.Models;

namespace Store.App.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<Category>> GetCategoriesWithProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
    }
}