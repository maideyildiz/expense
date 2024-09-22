using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Data.DbSettings;
var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var dbOptions = new DbOptions(config);

builder.Services.AddSingleton<IMongoDbContext>(provider => new MongoDbContext(dbOptions));

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

