using System.Data;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Services;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IDbConnection>(sp => new MySqlConnection(connectionString));

builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map your endpoints here, e.g., app.MapControllers() if you are using controllers
app.MapGet("/", () => "Hello World!");

app.Run();

