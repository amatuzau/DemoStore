using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Store.DAL.Models;

namespace Store.DAL.Ado
{
    public class CartAdoRepository : BaseAdoRepository<Cart>
    {
        public CartAdoRepository(IOptions<AdoOptions> options) : base(options)
        {
        }

        public override Task<IEnumerable<Cart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override async Task<Cart> Get(int id)
        {
            const string cartQuery = "SELECT Id FROM dbo.Cart WHERE Id = @id";
            const string itemsQuery = "SELECT ProductId, Amount FROM dbo.CartItem WHERE CartId = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(cartQuery, new SqlParameter("@id", id));
            Cart result = default;

            if (reader.HasRows)
            {
                await reader.CloseAsync();

                result = new Cart { Id = id, CartItems = new List<CartItem>() };

                reader = await ExecuteQueryAsync(itemsQuery, new SqlParameter("@id", id));
                if (reader.HasRows)
                    while (await reader.ReadAsync())
                        result.CartItems.Add(new CartItem
                        {
                            ProductId = reader.GetInt32(0),
                            Count = reader.GetInt32(1)
                        });
            }
            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Cart item)
        {
            const string query = "INSERT INTO dbo.Cart OUTPUT inserted.Id DEFAULT VALUES";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query);
            await connection.CloseAsync();
            return (int) result;
        }

        public override async Task Update(Cart item)
        {
            const string deleteQuery = "DElETE FROM dbo.CartItem WHERE CartId = @id";
            const string addQuery =
                "INSERT INTO dbo.CartItem(CartId, ProductId, Amount) VALUES (@cartId, @productId, @count)";

            await connection.OpenAsync();
            await ExecuteCommandAsync(deleteQuery, new SqlParameter("@id", item.Id));

            foreach (var cartItem in item.CartItems)
                await ExecuteCommandAsync(addQuery,
                    new SqlParameter("@cartId", item.Id),
                    new SqlParameter("@productId", cartItem.ProductId),
                    new SqlParameter("@count", cartItem.Count));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            await connection.OpenAsync();
            const string query = "DELETE FROM dbo.CartItem WHERE Id = @id";
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}