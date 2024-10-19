using ExpenseTracker.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Bağımlılıkları tek bir yerden yükle
builder.Services.AddProjectDependencies(builder.Configuration);

// Swagger/OpenAPI ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller'ları ekle
builder.Services.AddControllers();

var app = builder.Build();

// Geliştirme ortamında Swagger UI kullan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
