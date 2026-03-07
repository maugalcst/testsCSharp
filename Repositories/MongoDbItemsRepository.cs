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

        public void Create(Item item)
        {
            _collection.InsertOne(item);
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(item => item.Id == id);
        }

        public IEnumerable<Item> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Item GetById(Guid id)
        {
            return _collection.Find(item => item.Id == id).SingleOrDefault();
        }

        public void Update(Item item)
        {
            _collection.ReplaceOne(x => x.Id == item.Id, item);
        }
    }
}