using DependencyInjection.Entities;
using MongoDB.Driver;

namespace DependencyInjection.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string DatabaseName = "catalog";
        private const string CollectionName = "items";
        private readonly IMongoCollection<Item> _collection;


        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(DatabaseName);
            _collection = database.GetCollection<Item>(CollectionName);
        }

        public async Task CreateAsync(Item item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Item> GetByIdAsync(Guid id)
        {
            return await _collection.Find(item => item.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            await _collection.ReplaceOneAsync(x => x.Id == item.Id, item);
        }
    }
}