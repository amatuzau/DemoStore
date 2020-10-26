using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL
{
    public interface IRepository<T>: IDisposable
        where T: IEntity
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        int Add(T item);

        void Update(T item);

        void Delete(int id);

        Task SaveChangesAsync();
    }
}
