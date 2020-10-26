using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Store.DAL.Models;

namespace Store.DAL.Ado
{
    public class CategoriesAdoRepository : BaseAdoRepository<Category>
    {
        public CategoriesAdoRepository(IOptions<AdoOptions> options) : base(options)
        {
        }

        public override async Task<IEnumerable<Category>> GetAll()
        {
            const string query = "SELECT Id, Name FROM dbo.Category";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Category>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Category> Get(int id)
        {
            const string query = "SELECT Id, Name FROM dbo.Category WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));
            Category result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Category {Id = reader.GetInt32(0), Name = reader.GetString(1)};
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Category item)
        {
            const string query = "INSERT INTO dbo.Category(Name) OUTPUT Inserted.Id VALUES(@name)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query, new SqlParameter("@name", item.Name));
            await connection.CloseAsync();
            return (int) result;
        }

        public override async Task Update(Category item)
        {
            const string query = "UPDATE dbo.Category SET Name = @name WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@name", item.Name),
                new SqlParameter("@id", item.Id));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Category WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}