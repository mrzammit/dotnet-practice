using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Practice.Api.Tests;

public class ItemsEndpointTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ItemsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_items_returns_ok()
    {
        // Arrange
        using var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = false
            });

        // Act
        using var response = await client.GetAsync("/api/items");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}