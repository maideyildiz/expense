// using Moq;
// using MongoDB.Bson;
// using MongoDB.Driver;
// using Xunit;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using ExpenseTracker.Infrastructure.Helpers;
// using ExpenseTracker.Core.Models;
// using ExpenseTracker.Core.Repositories;
// using ExpenseTracker.Infrastructure.Data.DbSettings;
// namespace ExpenseTracker.Tests.Infrastructure.Test.User;

// public class UserTest : ExpenseTracker.Infrastructure.Services.BaseService<ExpenseTracker.Core.Models.User>
// {
//     private readonly Mock<IMongoCollection<ExpenseTracker.Core.Models.User>> _mockCollection;
//     private readonly Mock<IMongoDatabase> _mockDatabase;
//     private readonly Mock<IMongoClient> _mockClient;
//     private readonly ExpenseTracker.Infrastructure.Services.UserService _userService;
//     private DbOptions dbOption = new DbOptions(
//         "mongodb+srv://new:PvtvAYVRDL8UiabA@expensetrackertestdb.v9x2c.mongodb.net/",
//         "ExpenseTrackerTestDb",
//         "Users");

//     public UserTest() : base(dbOption, "Users")
//     {
//         _mockCollection = new Mock<IMongoCollection<ExpenseTracker.Core.Models.User>>();
//         _mockDatabase = new Mock<IMongoDatabase>();
//         _mockClient = new Mock<IMongoClient>();

//         _mockDatabase.Setup(db => db.GetCollection<ExpenseTracker.Core.Models.User>(It.IsAny<string>(), null))
//                       .Returns(_mockCollection.Object);
//         _mockClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null))
//                     .Returns(_mockDatabase.Object);

//         _userService = new ExpenseTracker.Infrastructure.Services.UserService(dbOption);
//     }

//     [Fact]
//     public async Task RegisterAsync_Should_Add_New_User()
//     {
//         // Arrange
//         var newUser = new ExpenseTracker.Core.Models.User { Email = "testuser", Password = "hashed_password" };
//         _mockCollection.Setup(c => c.InsertOneAsync(It.IsAny<ExpenseTracker.Core.Models.User>(), null, default)).Returns(Task.CompletedTask);

//         // Act
//         await _userService.RegisterAsync(newUser);

//         // Assert
//         _mockCollection.Verify(c => c.InsertOneAsync(It.IsAny<ExpenseTracker.Core.Models.User>(), null, default), Times.Once);
//     }
// }
