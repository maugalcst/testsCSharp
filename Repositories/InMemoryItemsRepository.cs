using DependencyInjection.Entities;

namespace DependencyInjection.Repositories
{
    public class InMemoryItemsRepository : IItemsRepository
    {
        private readonly List<Item> _items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Wilson Pro Staff V14", Price = 250, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Babolat Pure Aero", Price = 230, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Can of Tennis Balls", Price = 5, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetAll()
        {
            return _items;
        }

        public Item GetById(Guid id)
        {
            return _items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}