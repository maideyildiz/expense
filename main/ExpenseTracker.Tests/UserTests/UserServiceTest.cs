using Moq;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Data.DbSettings;
using MongoDB.Driver;
namespace ExpenseTracker.Tests.UserTests;
public class UserServiceTests
{
    private readonly Mock<IMongoDbContext> _mockContext;
    private readonly Mock<IMongoCollection<ExpenseTracker.Core.Models.User>> _mockUserCollection;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockContext = new Mock<IMongoDbContext>();
        _mockUserCollection = new Mock<IMongoCollection<User>>();

        _mockContext.Setup(c => c.GetCollection<User>("User")).Returns(_mockUserCollection.Object);
        _userService = new UserService(_mockContext.Object);
    }

    [Fact]
    public async Task RegisterAsync_Should_ThrowArgumentNullException_When_UserIsNull()
    {
        // Arrange
        User newUser = null;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.RegisterAsync(newUser));
    }

    [Fact]
    public async Task RegisterAsync_Should_ThrowArgumentException_When_UserEmailIsEmpty()
    {
        // Arrange
        var newUser = new User { Email = string.Empty, Password = "hashed_password" };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterAsync(newUser));
    }

    [Fact]
    public async Task RegisterAsync_Should_ThrowArgumentException_When_UserEmailIsAlreadyTaken()
    {
        // Arrange
        var existingUser = new User { Email = "existinguser@example.com", Password = "hashed_password" };

        var mockFindFluent = new Mock<IFindFluent<User, User>>();
        mockFindFluent.Setup(m => m.ToListAsync(default)).ReturnsAsync(new List<User> { existingUser });

        _mockUserCollection.Setup(repo => repo.Find(It.IsAny<FilterDefinition<User>>(), null))
            .Returns(mockFindFluent.Object);

        var newUser = new User { Email = existingUser.Email, Password = "hashed_password" };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterAsync(newUser));
    }

    [Fact]
    public async Task RegisterAsync_Should_ThrowArgumentException_When_UserPasswordIsEmpty()
    {
        // Arrange
        var newUser = new User { Email = "testuser@example.com", Password = string.Empty };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterAsync(newUser));
    }

    [Fact]
    public async Task LoginAsync_Should_ThrowArgumentNullException_When_UserIsNull()
    {
        // Arrange
        string email = null;
        string password = null;

        // Act and Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.LoginAsync(email, password));
        Assert.Equal("email", exception.ParamName); // Burada email parametresinin null olduğunu kontrol edin
    }


    [Fact]
    public async Task LoginAsync_Should_ThrowArgumentNullException_When_UserEmailIsEmpty()
    {
        // Arrange
        var user = new User { Email = string.Empty, Password = "hashed_password" };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.LoginAsync(user.Email, user.Password));
    }

    [Fact]
    public async Task LoginAsync_Should_ThrowArgumentNullException_When_UserPasswordIsEmpty()
    {
        // Arrange
        var user = new User { Email = "testuser@example.com", Password = string.Empty };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.LoginAsync(user.Email, user.Password));
    }
    [Fact]
    public async Task RegisterAsync_Should_RegisterUser_When_DataIsValid()
    {
        // Arrange
        var validUser = new User
        {
            Email = "test@example.com",
            Password = "hashedPassword" // Assume this is a hashed password.
        };

        // Mock the behavior of the _mockUserCollection
        _mockUserCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<User>>(), null))
            .Returns((IFindFluent<User, User>)new Mock<IAsyncCursor<User>>().Object); // Simulate an empty cursor

        _mockUserCollection.Setup(c => c.InsertOneAsync(It.IsAny<User>(), null, default))
            .Returns(Task.CompletedTask);  // Simulate successful insert

        // Setup the context to return the mock user collection
        _mockContext.Setup(c => c.GetCollection<User>("User")).Returns(_mockUserCollection.Object);

        // Act
        var result = await _userService.RegisterAsync(validUser);

        // Assert
        Assert.True(result); // Ensure registration is successful
        _mockUserCollection.Verify(c => c.InsertOneAsync(It.Is<User>(u => u.Email == validUser.Email), null, default), Times.Once);
    }


}

