using DependencyInjection.Entities;
using DependencyInjection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemoryItemsRepository _repository;

        public ItemsController(InMemoryItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPost("{id}")]
        public ActionResult<Item> GetById(Guid id)
        {
            var item = _repository.GetById(id);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }
    }
}

    