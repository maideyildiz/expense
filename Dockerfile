# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ExpenseTracker.Core/*.csproj ./ExpenseTracker.Core/
COPY ExpenseTracker.Infrastructure/*.csproj ./ExpenseTracker.Infrastructure/
COPY ExpenseTracker.API/*.csproj ./ExpenseTracker.API/
COPY ExpenseTracker.Tests/*.csproj ./ExpenseTracker.Tests/
RUN dotnet restore ExpenseTracker.API/ExpenseTracker.API.csproj

# Copy the entire project files and publish the release version
COPY . ./
RUN dotnet publish ExpenseTracker.API/ExpenseTracker.API.csproj -c Release -o out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published files from the build environment
COPY --from=build-env /app/out .

# Expose ports for HTTP and HTTPS
EXPOSE 80
EXPOSE 443

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ExpenseTracker.API.dll"]
