using Store.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL
{
    public class UnitOfWork: IDisposable
    {
        public UnitOfWork(string basePath)
        {
            CategoriesRepository = new GenericFileRepository<Category>(basePath);
            ProductsRepository = new GenericFileRepository<Product>(basePath);
            CartsRepository = new GenericFileRepository<Cart>(basePath);
        }

        public IRepository<Category> CategoriesRepository { get; private set; }
        public IRepository<Product> ProductsRepository { get; private set; }
        public IRepository<Cart> CartsRepository { get; private set; }

        public async Task SaveAsync()
        {
            await CategoriesRepository.SaveChangesAsync();
            await ProductsRepository.SaveChangesAsync();
            await CartsRepository.SaveChangesAsync();
        }
        public void Dispose()
        {
            CategoriesRepository.Dispose();
            ProductsRepository.Dispose();
            CartsRepository.Dispose();
        }
    }
}
