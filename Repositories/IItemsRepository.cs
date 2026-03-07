using DependencyInjection.Entities;

namespace DependencyInjection.Repositories
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(Guid id);
        Task CreateAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Guid id);

    }
}