using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

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
        using var client = _factory.CreateClient();

        using var response = await client.GetAsync("/api/items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_items_when_empty_returns_enpty_json_array()
    {
        using var client = _factory.CreateClient();

        using var response = await client.GetAsync("/api/items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("[]", body);
    }

    [Fact]
    public async Task Post_items_returns_created_item()
    {
        var expectedTitle = "Practice TDD";

        using var client = _factory.CreateClient();
        var request = new { title = expectedTitle };

        using var response = await client.PostAsJsonAsync("/api/items", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var item = await response.Content.ReadFromJsonAsync<CreateItemResponse>();

        Assert.NotNull(item);
        Assert.NotEqual(Guid.Empty, item.Id);
        Assert.Equal(expectedTitle, item.Title);
        Assert.False(item.isComplete);
    }

    public sealed record CreateItemResponse(Guid Id, string Title, bool isComplete);
}