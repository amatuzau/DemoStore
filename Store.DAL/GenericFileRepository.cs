
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.DAL
{
    public class GenericFileRepository<T> : IRepository<T>
        where T : IEntity
    {
        private const string BasePath = "data";
        private bool disposedValue;
        private readonly Dictionary<int, T> data = new Dictionary<int, T>();
        private string GetFilePath => Path.ChangeExtension(Path.Combine(BasePath, typeof(T).Name), ".json");
        private int lastId;
        private int GetNewId => ++lastId;

        public GenericFileRepository()
        {
            if (File.Exists(GetFilePath)) {
                using var file = File.OpenRead(GetFilePath);
                var items = JsonSerializer.DeserializeAsync<IEnumerable<T>>(file).Result;
                data = items.ToDictionary(i => i.Id);
                lastId = data.Max(i => i.Key);
            }
        }

        public int Add(T item)
        {
            var newId = GetNewId;
            item.Id = newId;
            data.Add(newId, item);
            return newId;
        }

        public void Delete(int id)
        {
            data.Remove(id);
        }

        public T Get(int id)
        {
            return data[id];
        }

        public IEnumerable<T> GetAll()
        {
            return data.Values.ToArray();
        }

        public async Task SaveChangesAsync()
        {
            if(!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
            using var file = File.Open(GetFilePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(file, data.Values, new JsonSerializerOptions { WriteIndented = true });
        }

        public void Update(T item)
        {
            if (data.ContainsKey(item.Id))
            {
                data[item.Id] = item;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GenericFileRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
