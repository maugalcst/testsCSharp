using DependencyInjection.Entities;

namespace DependencyInjection.Dtos
{
    public record ItemDto(
        Guid Id,
        string Name,
        decimal Price,
        DateTimeOffset CreatedDate
    );

    public record CreateItemDto(
        string Name, 
        decimal Price
    );

    public record UpdateItemDto(
        string Name,
        decimal Price
    );

    public static class ItemExtensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Price, item.CreatedDate);
        }
    }
}