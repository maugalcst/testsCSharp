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
        public IEnumerable<ItemDto> GetAll()
        {
            return _repository.GetAll().Select(item => item.AsDto());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = _repository.GetById(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<CreateItemDto> CreateItem(CreateItemDto item)
        {
            var newItem = new Item 
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Price = item.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.Create(newItem);

            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem.AsDto());

        }

        [HttpPut("{id}")]
        public ActionResult<UpdateItemDto> UpdateItem(Guid id, UpdateItemDto item)
        {
               var existingItem = _repository.GetById(id);

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

               _repository.Update(updatedItem);

               return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var itemToDelete = _repository.GetById(id);

            if (itemToDelete is null)
            {
                return NotFound();
            }

            _repository.Delete(itemToDelete.Id);
            return NoContent();
        }

    }
}

    