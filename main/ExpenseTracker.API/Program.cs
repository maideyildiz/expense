using ExpenseTracker.API.Common.Errors;
using ExpenseTracker.API.Extensions;
using ExpenseTracker.API.Middleware;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Tüm originlere izin ver
               .AllowAnyMethod() // Tüm HTTP yöntemlerine izin ver
               .AllowAnyHeader(); // Tüm başlıklara izin ver
    });
});
// Bağımlılıkları tek bir yerden yükle
builder.Services.AddProjectDependencies(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Swagger/OpenAPI ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller'ları ekle
builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, ExpenseTrackerProblemDetailsFactory>();

var app = builder.Build();

// Geliştirme ortamında Swagger UI kullan
if (app.Environment.IsDevelopment())
{
    // Swagger'ı etkinleştir
    app.UseSwagger();

    // Swagger UI'yi etkinleştir
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseTracker API");
        c.RoutePrefix = string.Empty; // Swagger'ı kök dizinde göstermek için
    });
}
app.UseCors("AllowAllOrigins");
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseExceptionHandler("error/");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
