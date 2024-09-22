using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Data.DbSettings;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DbOptions>(builder.Configuration);
// Add services to the container.
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

