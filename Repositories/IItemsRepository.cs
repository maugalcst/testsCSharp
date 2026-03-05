using DependencyInjection.Entities;

namespace DependencyInjection.Repositories
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetAll();
        Item GetById(Guid id);
        void Create(Item item);
        void Update(Item item);
        void Delete(Guid id);

    }
}