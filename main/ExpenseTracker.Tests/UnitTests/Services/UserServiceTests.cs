using Moq;
using Xunit;
using System.Threading.Tasks;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Data.DbSettings;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ExpenseTracker.Tests.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IMongoDbContext> _mockContext;
    private readonly Mock<IMongoCollection<User>> _mockUserCollection;
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
        var newUser = new User { Email = "testuser@example.com", Password = "hashed_password" };

        // Mocking the Find method to return an IFindFluent<User, User>
        var mockFindFluent = new Mock<IFindFluent<User, User>>();

        // Set up the ToListAsync() method on the IFindFluent mock
        mockFindFluent.Setup(m => m.ToListAsync(default))
            .ReturnsAsync(new List<User>()); // Boş bir kullanıcı listesi döndürüyoruz

        _mockUserCollection.Setup(repo => repo.Find(It.IsAny<FilterDefinition<User>>(), null))
            .Returns(mockFindFluent.Object); // Mock FindFluent nesnesini döndürüyoruz

        // Act
        await _userService.RegisterAsync(newUser);

        // Assert
        _mockUserCollection.Verify(m => m.InsertOneAsync(It.IsAny<User>(), null, default), Times.Once);
    }




}

