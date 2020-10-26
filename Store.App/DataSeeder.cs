using Store.DAL;
using Store.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore
{
    public static class DataSeeder
    {
        private static List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Sport" },
            };

        private static List<Product> products = new List<Product>
            {
                new Product { Id = 1, Category = categories[0], Name = "iPhone 11", Image = "iphone11.jpg", Price = 1120m },
                new Product { Id = 2, Category = categories[0], Name = "Huawei P40", Image = "p40.jpg", Price = 860m },
                new Product { Id = 3, Category = categories[0], Name = "Xiaomi Mi 10 ", Image = "mi10.jpg", Price = 790m },
                new Product { Id = 4, Category = categories[0], Name = "Galaxy S 20", Image = "s20.jpg", Price = 1050m },
                new Product { Id = 5, Category = categories[1], Name = "Bicycle GT 3.0", Image = "gt30.jpg", Price = 430m },
                new Product { Id = 6, Category = categories[1], Name = "Wilson Voleyball", Image = "wilson.jpg", Price = 30m },
            };

        public static async Task<IRepository<Category>> CreateCategoriesRepoAsync()
        {
            var repo = new GenericFileRepository<Category>();
            categories.ForEach(c => repo.Add(c));
            await repo.SaveChangesAsync();
            return repo;
        }

        public static async Task<IRepository<Product>> CreateProductsRepoAsync()
        {
            var repo = new GenericFileRepository<Product>();
            products.ForEach(c => repo.Add(c));
            await repo.SaveChangesAsync();
            return repo;
        }
    }
}
