using Store.DAL;
using Store.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Core
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Category> categoriesRepo;
        private readonly IRepository<Product> productsRepo;

        public ProductsService(IRepository<Category> categoriesRepo, IRepository<Product> productsRepo)
        {
            this.categoriesRepo = categoriesRepo;
            this.productsRepo = productsRepo;
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return productsRepo.GetAll().Select(p => p.Category).Distinct();
        }

        public IEnumerable<Product> GetProducts()
        {
            return productsRepo.GetAll();
        }

        public Product GetProductById(int id)
        {
            return productsRepo.Get(id);
        }
    }
}
