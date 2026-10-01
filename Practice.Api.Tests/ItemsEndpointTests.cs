using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Practice.Api.Tests;

public class ItemsEndpointTests
    : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory = new();

    public void Dispose()
    {
        _factory.Dispose();
    }

    [Fact]
    public async Task Get_items_returns_ok()
    {
        using var client = _factory.CreateClient();

        using var response = await client.GetAsync("/api/items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_items_when_empty_returns_empty_json_array()
    {
        using var client = _factory.CreateClient();

        using var response = await client.GetAsync("/api/items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("[]", body);
    }

    [Fact]
    public async Task Get_item_by_id_returns_created_item()
    {
        using var client = _factory.CreateClient();

        using var postResponse = await client.PostAsJsonAsync("/api/items", new { title = "Find This Item" });

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var createdItem = await postResponse.Content.ReadFromJsonAsync<CreateItemResponse>();

        Assert.NotNull(createdItem);

        using var getResponse = await client.GetAsync($"/api/items/{createdItem.Id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var retrievedItems = await getResponse.Content.ReadFromJsonAsync<CreateItemResponse>();

        Assert.NotNull(retrievedItems);
        Assert.Equal(createdItem, retrievedItems);
    }

    [Fact]
    public async Task Get_item_by_id_when_missing_returns_not_found()
    {
        using var client = _factory.CreateClient();

        using var response =
            await client.GetAsync($"/api/items/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_item_returns_created_item()
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
        Assert.False(item.IsComplete);
    }

    [Fact]
    public async Task Post_items_then_get_items_returns_created_item()
    {
        var expectedTitle = "Practice TDD";

        using var client = _factory.CreateClient();
        var postRequest = new { title = expectedTitle };

        using var postResponse = await client.PostAsJsonAsync("/api/items", postRequest);

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var createdItem = await postResponse.Content.ReadFromJsonAsync<CreateItemResponse>();

        Assert.NotNull(createdItem);

        using var getResponse = await client.GetAsync("/api/items");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var items = await getResponse.Content.ReadFromJsonAsync<List<CreateItemResponse>>();

        Assert.NotNull(items);

        var retrievedItem = Assert.Single(items, item => item.Id == createdItem.Id);

        Assert.Equal(createdItem.Title, retrievedItem.Title);
        Assert.Equal(createdItem.IsComplete, retrievedItem.IsComplete);
    }

    public sealed record CreateItemResponse(Guid Id, string Title, bool IsComplete);
}