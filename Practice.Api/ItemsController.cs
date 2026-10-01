using Microsoft.AspNetCore.Mvc;
using Practice.Api.Models;

namespace Practice.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> Items = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Array.Empty<object>());
        }

        [HttpPost]
        public IActionResult Create(CreateItemRequest request)
        {
            var item = new Item(Guid.NewGuid(), request.Title, false);
            Items.Add(item);
            return Created($"api/items/{item.Id}", item);
        }

    }
}
