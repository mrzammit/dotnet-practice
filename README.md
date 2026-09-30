[![.NET](https://github.com/mrzammit/dotnet-practice/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mrzammit/dotnet-practice/actions/workflows/dotnet.yml)

# .NET Practice

A place to refresh my C# and .NET development skills and explore new ones through hands-on experimentation.

This is an evolving learning repository rather than a finished application. Exercises and features will grow as I explore different topics.

## Current focus

- ASP.NET Core APIs using controllers and minimal API endpoints
- Unit testing with xUnit
- Integration testing with `WebApplicationFactory`
- Request routing and HTTP responses
- Test-driven development as new behavior is added

## Projects

| Project | Purpose |
| --- | --- |
| `Practice.Api` | An ASP.NET Core API targeting .NET 10 |
| `Practice.Api.Tests` | xUnit unit and integration tests |

Unit tests call controller methods directly. Integration tests send HTTP requests through an in-memory test host to exercise routing and endpoint behavior.

## Getting started

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0). Visual Studio, VS Code, or Rider can be used, but the commands below only require the SDK.

Clone the repository:

```shell
git clone https://github.com/mrzammit/dotnet-practice.git
cd dotnet-practice
```

Restore packages and build:

```shell
dotnet restore
dotnet build
```

## Run the API

From the repository root:

```shell
dotnet run --project Practice.Api --no-launch-profile --urls http://localhost:5050
```

Current endpoints:

| Method | Route | Response |
| --- | --- | --- |
| GET | `/api/ping` | `200 OK` with the text `pong` |
| GET | `/api/items` | `200 OK` with an empty body |

Open <http://localhost:5050/api/ping> in a browser to try the ping endpoint.

## Run the tests

From the repository root:

```shell
dotnet test
```

The integration tests start their own test host, so the API does not need to be running separately.

## Learning approach

Start with small examples, add tests, and build up behavior incrementally. The goal is to understand the code, practice development techniques, and keep a record of what I learn along the way.
