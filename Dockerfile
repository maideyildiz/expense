# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

# Copy the .csproj files and restore dependencies
COPY ["src/ExpenseTracker.API/ExpenseTracker.API.csproj", "src/ExpenseTracker.API/"]
COPY ["src/ExpenseTracker.Application/ExpenseTracker.Application.csproj", "src/ExpenseTracker.Application/"]
COPY ["src/ExpenseTracker.Contracts/ExpenseTracker.Contracts.csproj", "src/ExpenseTracker.Contracts/"]
COPY ["src/ExpenseTracker.Core/ExpenseTracker.Core.csproj", "src/ExpenseTracker.Core/"]
COPY ["src/ExpenseTracker.Infrastructure/ExpenseTracker.Infrastructure.csproj", "src/ExpenseTracker.Infrastructure/"]
RUN dotnet restore src/ExpenseTracker.API/ExpenseTracker.API.csproj

# Copy the entire project files and publish the release version
COPY . ./
RUN dotnet publish src/ExpenseTracker.API/ExpenseTracker.API.csproj -c Release -o /app/out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy the published files from the build environment
COPY --from=build-env /app/out .

# Expose ports for HTTP and HTTPS
EXPOSE 80
EXPOSE 443

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ExpenseTracker.API.dll"]
