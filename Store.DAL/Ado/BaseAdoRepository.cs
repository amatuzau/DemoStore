using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Store.DAL.Ado
{
    public abstract class BaseAdoRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly SqlConnection connection;

        protected BaseAdoRepository(IOptions<AdoOptions> options)
        {
            connection = new SqlConnection(options.Value.ConnectionString);
        }

        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task<T> Get(int id);
        public abstract Task<int> Add(T item);
        public abstract Task Update(T item);
        public abstract Task Delete(int id);

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected async Task<SqlDataReader> ExecuteQueryAsync(string query, params SqlParameter[] parameters)
        {
            var command = new SqlCommand {CommandText = query, Connection = connection};
            command.Parameters.AddRange(parameters);
            return await command.ExecuteReaderAsync();
        }

        protected async Task<int> ExecuteCommandAsync(string query, params SqlParameter[] parameters)
        {
            var command = new SqlCommand {CommandText = query, Connection = connection};
            command.Parameters.AddRange(parameters);
            return await command.ExecuteNonQueryAsync();
        }

        protected async Task<object> ExecuteScalarAsync(string query, params SqlParameter[] parameters)
        {
            var command = new SqlCommand {CommandText = query, Connection = connection};
            command.Parameters.AddRange(parameters);
            return await command.ExecuteScalarAsync();
        }

        private void ReleaseUnmanagedResources()
        {
            connection.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing) connection.Dispose();
        }

        ~BaseAdoRepository()
        {
            Dispose(false);
        }
    }
}