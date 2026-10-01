using Microsoft.AspNetCore.Mvc;
using Practice.Api.Repositories;

namespace Practice.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemRepository _repository;

        public ItemsController(ItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var item = _repository.Get(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(CreateItemRequest request)
        {
            var item = _repository.Add(request.Title);
            return Created($"/api/items/{item.Id}", item);
        }

    }
}
