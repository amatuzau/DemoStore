﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DAL;
using Store.DAL.Models;

namespace Store.App
{
    public class DataSeeder
    {
        private static readonly List<Category> categories = new List<Category>
        {
            new Category {Name = "Electronics"},
            new Category {Name = "Sport"}
        };

        private static readonly List<Product> products = new List<Product>
        {
            new Product {Category = categories[0], Name = "iPhone 11", Image = "iphone11.jpg", Price = 1120m},
            new Product {Category = categories[0], Name = "Huawei P40", Image = "p40.jpg", Price = 860m},
            new Product {Category = categories[0], Name = "Xiaomi Mi 10 ", Image = "mi10.jpg", Price = 790m},
            new Product {Category = categories[0], Name = "Galaxy S 20", Image = "s20.jpg", Price = 1050m},
            new Product {Category = categories[1], Name = "Bicycle GT 3.0", Image = "gt30.jpg", Price = 430m},
            new Product {Category = categories[1], Name = "Wilson Volleyball", Image = "wilson.jpg", Price = 30m}
        };

        private readonly StoreContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<StoreUser> userManager;

        public DataSeeder(StoreContext context, UserManager<StoreUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
                await roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
            if (await roleManager.FindByNameAsync("User") == null)
                await roleManager.CreateAsync(new IdentityRole {Name = "User"});

            if (await userManager.FindByNameAsync("user1@example.com") == null)
            {
                var user = new StoreUser {UserName = "user1@example.com", Email = "user1@example.com", Cart = new Cart()};
                await userManager.CreateAsync(user, "user123");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (await userManager.FindByNameAsync("user2@example.com") == null)
            {
                var user = new StoreUser {UserName = "user2@example.com", Email = "user2@example.com", Cart = new Cart()};
                await userManager.CreateAsync(user, "user123");
                await userManager.AddToRoleAsync(user, "User");
            }

            if (!context.Categories.Any()) await context.Categories.AddRangeAsync(categories);

            if (!context.Products.Any()) await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }
}
