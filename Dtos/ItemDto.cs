using DependencyInjection.Entities;
using System.ComponentModel.DataAnnotations;

namespace DependencyInjection.Dtos
{
    public record ItemDto(
        Guid Id,
        string Name,
        decimal Price,
        DateTimeOffset CreatedDate
    );

    public record CreateItemDto(
        [Required] string Name, 
        [Range(1, 3000)] decimal Price
    );

    public record UpdateItemDto(
        [Required] string Name, 
        [Range(1, 3000)] decimal Price
    );

    public static class ItemExtensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Price, item.CreatedDate);
        }
    }
}