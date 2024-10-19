using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Services;
using Moq;
using Xunit;

namespace ExpenseTracker.Tests.Unit.Services;

public class ExpenseServiceTests
{
    private readonly Mock<IDatabaseConnection> _mockDatabaseConnection;
    private readonly ExpenseService _expenseService;

    public ExpenseServiceTests()
    {
        _mockDatabaseConnection = new Mock<IDatabaseConnection>();
        _expenseService = new ExpenseService(_mockDatabaseConnection.Object);
    }

    [Fact]
    public async Task AddExpenseAsync_ReturnsAffectedRows_WhenExpenseIsAdded()
    {
        // Arrange
        Category category = new Category { Id = Guid.NewGuid(), Name = "Test Category" };
        User user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "nZsZB@example.com", Password = "password123" };
        var expense = new Expense { Id = Guid.NewGuid(), Amount = 100, Description = "Test Expense", Category = category, User = user };
        _mockDatabaseConnection.Setup(db => db.ExecuteAsync(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.AddAsync(expense);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetExpensesAsync_ReturnsExpenses_WhenExpensesExist()
    {
        // Arrange
        Category category = new Category { Id = Guid.NewGuid(), Name = "Test Category" };
        User user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "nZsZB@example.com", Password = "password123" };
        var expenses = new[]
        {
        new Expense { Id = Guid.NewGuid(), Amount = 100, Description = "Expense 1", Category = category, User = user },
        new Expense { Id = Guid.NewGuid(), Amount = 200, Description = "Expense 2", Category = category, User = user }
    };

        _mockDatabaseConnection.Setup(db => db.QueryAsync<Expense>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(expenses);

        // Act
        var result = await _expenseService.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count()); // Count() uzantı metodunu kullan
    }

    [Fact]
    public async Task GetExpenseByIdAsync_ReturnsExpense_WhenExpenseExists()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        Category category = new Category { Id = Guid.NewGuid(), Name = "Test Category" };
        User user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "nZsZB@example.com", Password = "password123" };
        var expense = new Expense { Id = expenseId, Amount = 100, Description = "Test Expense", Category = category, User = user };
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<Expense>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(expense);

        // Act
        var result = await _expenseService.GetByIdAsync(expenseId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expenseId, result.Id);
        Assert.Equal(100, result.Amount);
    }

    [Fact]
    public async Task GetExpenseByIdAsync_ReturnsNull_WhenExpenseDoesNotExist()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<Expense>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync((Expense?)null);

        // Act
        var result = await _expenseService.GetByIdAsync(expenseId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateExpenseAsync_ReturnsAffectedRows_WhenExpenseIsUpdated()
    {
        // Arrange
        Category category = new Category { Id = Guid.NewGuid(), Name = "Updated Category" };
        User user = new User { Id = Guid.NewGuid(), Name = "Updated User", Email = "updated@example.com", Password = "updatedpassword123" };
        var expense = new Expense { Id = Guid.NewGuid(), Amount = 150, Description = "Updated Expense", Category = category, User = user };
        _mockDatabaseConnection.Setup(db => db.ExecuteAsync(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.UpdateAsync(expense);

        // Assert
        Assert.Equal(1, result);
    }
}
