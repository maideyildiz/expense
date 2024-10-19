using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Services;

public class CategoryService : BaseService<Category>, ICategoryService
{
    private readonly IDatabaseConnection _dbConnection;
    public CategoryService(IDatabaseConnection dbConnection) : base(dbConnection, "Category")
    {
        _dbConnection = dbConnection;
    }

    public async Task<Category> AddCategoryIfNotExistsAsync(string categoryName)
    {
        // Kategoriyi kontrol et
        var existingCategory = await _dbConnection.QueryFirstOrDefaultAsync<Category>(
            "SELECT * FROM Categories WHERE Name = @Name", new { Name = categoryName });

        if (existingCategory != null)
        {
            // Eğer kategori varsa mevcut kategoriyi dön
            return existingCategory;
        }

        // Eğer kategori yoksa, yeni kategoriyi ekle
        var newCategory = new Category { Name = categoryName };

        var insertQuery = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description);";

        await _dbConnection.ExecuteAsync(insertQuery, newCategory);

        // Eklenen kategoriyi geri dön
        return newCategory;
    }
}


