[![.NET](https://github.com/mrzammit/dotnet-practice/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mrzammit/dotnet-practice/actions/workflows/dotnet.yml)

# .NET Practice

A place to refresh my C# and .NET development skills through hands-on experimentation.

This is an evolving learning repository rather than a finished application. Features grow through small test-driven steps, with an emphasis on understanding the code and the reasons behind each change.

## Current focus

- ASP.NET Core APIs using controllers and minimal API endpoints
- Dependency injection and service lifetimes
- In-memory storage using a repository
- Integration testing with xUnit and `WebApplicationFactory`
- HTTP routing, status codes, and JSON responses
- Test-driven development: red, green, refactor
- Keeping tests independent through application isolation

## Projects

| Project | Purpose |
| --- | --- |
| `Practice.Api` | ASP.NET Core API targeting .NET 10 |
| `Practice.Api.Tests` | xUnit tests, currently covering behavior through integration tests |

## Getting started

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0). Visual Studio, VS Code, or Rider can be used, but these commands only require the SDK.

```shell
git clone https://github.com/mrzammit/dotnet-practice.git
cd dotnet-practice
dotnet restore
dotnet build
```

## Run the API

From the repository root:

```shell
dotnet run --project Practice.Api --no-launch-profile --urls http://localhost:5050
```

### Current endpoints

| Method | Route | Response |
| --- | --- | --- |
| GET | `/api/ping` | `200 OK` with the text `pong` |
| GET | `/api/items` | `200 OK` with a JSON array of stored items; `[]` when empty |
| POST | `/api/items` | `201 Created` with the created item and a `Location` header |

To create an item, send this JSON body to `POST /api/items` with `Content-Type: application/json`:

```json
{
  "title": "Practice TDD"
}
```

The response contains a generated ID, the supplied title, and `isComplete` set to `false`:

```json
{
  "id": "5a84af37-b8ec-477c-9cb9-096e30fc4f5f",
  "title": "Practice TDD",
  "isComplete": false
}
```

The ID above is illustrative. Each created item receives a new ID.

GET by ID is not implemented yet, so the URL in the `Location` header cannot currently be used to retrieve an individual item.

## How storage works

`ItemsController` handles HTTP requests and delegates item creation and storage to `ItemRepository`, which is registered as a singleton through dependency injection.

One repository instance is shared across requests within an application instance. `GetAll()` returns an array snapshot rather than exposing the underlying list.

Storage is in memory only. Items are lost when the application restarts, and separate application instances do not share data. The current list implementation does not synchronize concurrent access; that is a future learning exercise.

## Run the tests

From the repository root:

```shell
dotnet test
```

Integration tests start their own in-memory application host through `WebApplicationFactory<Program>`. The API does not need to be running separately.

### Current coverage

- Ping returns `200 OK` and `pong`.
- GET items returns `200 OK`.
- An empty item collection returns a JSON array of `[]`.
- POST returns `201 Created`, a nonempty ID, the expected title, and `isComplete = false`.
- An item created through POST can be retrieved in the collection through GET.

Each test in `ItemsEndpointTests` gets its own factory and application instance. This gives each test a fresh repository while allowing multiple requests within one test to share stored items. The factory is disposed after the test.

`GeneralEndpointTests` uses a shared class fixture for the stateless ping endpoint. `ItemsControllerTests` is currently a placeholder with no unit tests.

## Continuous integration

The GitHub Actions workflow restores dependencies, builds the solution, and runs the tests on pushes and pull requests to `main` using .NET 10.

## Learning approach

1. Choose one observable behavior.
2. Write a test and confirm it fails for the expected reason.
3. Implement enough code to make it pass.
4. Refactor while keeping the tests green.

Keep the code explicit and easy to follow. Add abstractions and test helpers when they solve a concrete problem.

Possible next exercises include retrieving an item by ID, returning `404 Not Found` for an unknown ID, validating titles, updating and deleting items, and making repository access safe for concurrent requests.