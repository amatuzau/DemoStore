﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Store.DAL.Models;

namespace Store.DAL.Ado
{
    public class ProductsAdoRepository : BaseAdoRepository<Product>
    {
        public ProductsAdoRepository(IOptions<AdoOptions> options) : base(options)
        {
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            const string query = "SELECT Id, CategoryId, Name, Image, Price FROM dbo.Product";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Product>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        CategoryId = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Image = reader.GetString(3),
                        Price = reader.GetDecimal(4)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Product> Get(int id)
        {
            const string query = "SELECT Id, CategoryId, Name, Image, Price FROM dbo.Product WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            Product result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Product
                {
                    Id = reader.GetInt32(0),
                    CategoryId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Image = reader.GetString(3),
                    Price = reader.GetDecimal(4)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Product item)
        {
            const string query =
                "INSERT INTO dbo.Product(CategoryId, Name, Image, Price) OUTPUT Inserted.Id VALUES(@categoryId, @name, @image, @price)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@categoryId", item.CategoryId),
                new SqlParameter("@name", item.Name),
                new SqlParameter("@image", item.Image),
                new SqlParameter("@price", item.Price)
            );
            await connection.CloseAsync();
            return (int) result;
        }

        public override async Task Update(Product item)
        {
            const string query =
                "UPDATE dbo.Product SET CategoryId = @categoryId, Name = @name, Image = @image, Price = @price  WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@categoryId", item.CategoryId),
                new SqlParameter("@name", item.Name),
                new SqlParameter("@image", item.Image),
                new SqlParameter("@price", item.Price),
                new SqlParameter("@id", item.Id));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Product WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}