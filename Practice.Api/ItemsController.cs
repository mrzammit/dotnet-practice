using Microsoft.AspNetCore.Mvc;

namespace Practice.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Array.Empty<object>());
        }

        [HttpPost]
        public IActionResult Create(CreateItemRequest request)
        {
            var item = new
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                IsComplete = false
            };

            return StatusCode(StatusCodes.Status201Created, item);
        }

    }
}
