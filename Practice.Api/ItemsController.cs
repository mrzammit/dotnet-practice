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

    }
}
