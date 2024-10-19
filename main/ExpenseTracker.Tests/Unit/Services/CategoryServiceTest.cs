using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Services;
using Moq;
using Xunit;

namespace ExpenseTracker.Tests.Unit.Services;

public class CategoryServiceTests
{
    private readonly Mock<IDatabaseConnection> _mockDatabaseConnection;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _mockDatabaseConnection = new Mock<IDatabaseConnection>();
        _categoryService = new CategoryService(_mockDatabaseConnection.Object);
    }

    [Fact]
    public async Task AddCategoryAsync_ReturnsAffectedRows_WhenCategoryIsAdded()
    {
        // Arrange
        var category = new Category { Id = Guid.NewGuid(), Name = "New Category" };
        _mockDatabaseConnection.Setup(db => db.ExecuteAsync(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(1); // 1 satır eklendiğini varsayıyoruz.

        // Act
        var result = await _categoryService.AddAsync(category);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnsCategories_WhenCategoriesExists()
    {
        // Arrange
        var categories = new[] { new Category { Id = Guid.NewGuid(), Name = "Category 1" }, new Category { Id = Guid.NewGuid(), Name = "Category 2" } };
        _mockDatabaseConnection.Setup(db => db.QueryAsync<Category>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(categories);

        // Act
        var result = await _categoryService.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ReturnsCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new Category { Id = categoryId, Name = "Test Category" };
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<Category>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(category);

        // Act
        var result = await _categoryService.GetByIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        Assert.Equal("Test Category", result.Name);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_ReturnsNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<Category>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync((Category?)null); // Kategori bulunamadığını varsayıyoruz.

        // Act
        var result = await _categoryService.GetByIdAsync(categoryId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateCategoryAsync_ReturnsAffectedRows_WhenCategoryIsUpdated()
    {
        // Arrange
        var category = new Category { Id = Guid.NewGuid(), Name = "Updated Category" };
        _mockDatabaseConnection.Setup(db => db.ExecuteAsync(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(1); // 1 satır güncellendiğini varsayıyoruz.

        // Act
        var result = await _categoryService.UpdateAsync(category);

        // Assert
        Assert.Equal(1, result);
    }
}
