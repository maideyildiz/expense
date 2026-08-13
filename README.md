# Expense

Expense is a .NET 10 API for tracking personal expenses. The current goal is a small, usable API: authenticate, manage expenses, and view a monthly summary.

## Features

- **API**: .NET 10 with MediatR, Mapster, and Dapper.
- **Database**: MySQL managed with Docker.
- **Testing**: Unit tests using xUnit and Moq.

## Run locally

Prerequisites: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and Docker.

1. Create your local Docker environment file:

   ```bash
   cp .env.example .env
   ```

2. Start MySQL:

   ```bash
   docker compose up -d db
   ```

3. Restore packages and start the API:

   ```bash
   dotnet restore ExpenseTracker.sln
   dotnet run --project src/ExpenseTracker.API
   ```

The API applies its database migrations on startup. In Development, Swagger opens at `http://localhost:5222`.

Redis caching is disabled by default, so it is not required for local development.
MySQL is exposed on port `3307` to avoid conflicting with any MySQL service already running on your machine.

## Planned Features

- Monthly expense summary by category.
- A lightweight web client.
- Optional Redis caching after the core API is stable.
- Experimental: receipt recognition and voice input.
