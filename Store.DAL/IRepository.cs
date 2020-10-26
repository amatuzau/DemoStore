using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DAL
{
    public interface IRepository<T> : IDisposable
        where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<int> Add(T item);

        Task Update(T item);

        Task Delete(int id);

        Task SaveChangesAsync();
    }
}