using Store.DAL.Models;
using System.Collections.Generic;

namespace Store.Core
{
    public interface IProductsService
    {
        IEnumerable<Category> GetCategoriesWithProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
    }
}