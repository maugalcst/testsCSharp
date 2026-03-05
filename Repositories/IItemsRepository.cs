using DependencyInjection.Entities;

namespace DependencyInjection.Repositories
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetAll();
        Item GetById(Guid id);
    }
}