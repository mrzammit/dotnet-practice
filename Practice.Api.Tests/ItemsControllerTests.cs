using Microsoft.AspNetCore.Mvc;

namespace Practice.Api.Tests
{
    public class ItemsControllerTests
    {
        [Fact]
        public void Get_returns_ok()
        {
            var controller = new ItemsController();
            var response = controller.Get();

            Assert.IsType<OkResult>(response);
        }
    }
}
