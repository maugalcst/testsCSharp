using DependencyInjection.Dtos;
using DependencyInjection.Entities;
using DependencyInjection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).Select(item => item.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CreateItemDto>> CreateItem(CreateItemDto item)
        {
            var newItem = new Item 
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Price = item.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateAsync(newItem);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newItem.Id }, newItem.AsDto());

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto item)
        {
               var existingItem = await _repository.GetByIdAsync(id);

               if (existingItem is null) {
                    return NotFound();
               }

               var updatedItem = new Item
               {
                   Id = id,
                   Name = item.Name,
                   Price = item.Price,
                   CreatedDate = existingItem.CreatedDate
               };

               await _repository.UpdateAsync(updatedItem);

               return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var itemToDelete = await _repository.GetByIdAsync(id);

            if (itemToDelete is null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(itemToDelete.Id);
            return NoContent();
        }

    }
}

    