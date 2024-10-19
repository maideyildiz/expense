using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Services;
using Moq;

public class UserServiceTests
{
    private readonly Mock<IDatabaseConnection> _mockDatabaseConnection;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockDatabaseConnection = new Mock<IDatabaseConnection>();
        _userService = new UserService(_mockDatabaseConnection.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "Test User", Email = "nZsZB@example.com", Password = "password123" };
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<User>(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task AddUserAsync_ReturnsAffectedRows_WhenUserIsAdded()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Name = "New User", Email = "nZsZB@example.com", Password = "password123" };
        _mockDatabaseConnection.Setup(db => db.ExecuteAsync(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(1); // 1 satır eklendiğini varsayıyoruz.

        // Act
        var result = await _userService.AddAsync(user);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockDatabaseConnection.Setup(db => db.QueryFirstOrDefaultAsync<User>(
            It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync((User?)null); // Kullanıcı bulunamadığını varsayıyoruz.

        // Act
        var result = await _userService.GetByIdAsync(userId);

        // Assert
        Assert.Null(result);
    }
}
