using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.DAL
{
    public sealed class GenericFileRepository<T> : IRepository<T>
        where T : IEntity
    {
        private readonly Dictionary<int, T> data = new Dictionary<int, T>();
        private readonly string basePath;
        private int lastId;

        public GenericFileRepository(string basePath)
        {
            this.basePath = basePath;
            if (!File.Exists(GetFilePath)) return;
            using var file = File.OpenRead(GetFilePath);
            var items = JsonSerializer.DeserializeAsync<IEnumerable<T>>(file).Result;
            data = items.ToDictionary(i => i.Id);
            if (data.Any())
            {
                lastId = data.Max(i => i.Key);
            } else
            {
                lastId = -1;
            }
        }

        private string GetFilePath => Path.ChangeExtension(Path.Combine(basePath, typeof(T).Name), ".json");
        private int GetNewId => ++lastId;

        public Task<int> Add(T item)
        {
            var newId = GetNewId;
            item.Id = newId;
            data.Add(newId, item);
            return Task.FromResult(newId);
        }

        public Task Delete(int id)
        {
            data.Remove(id);
            return Task.CompletedTask;
        }

        public Task<T> Get(int id)
        {
            return Task.FromResult(data[id]);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult((IEnumerable<T>) data.Values.ToArray());
        }

        public async Task SaveChangesAsync()
        {
            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
            await using var file = File.Open(GetFilePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(file, data.Values, new JsonSerializerOptions {WriteIndented = true});
        }

        public Task Update(T item)
        {
            if (data.ContainsKey(item.Id)) data[item.Id] = item;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}