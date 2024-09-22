// using MongoDB.Bson;
// using MongoDB.Driver;
// using Xunit;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using ExpenseTracker.Core.Models;
// using ExpenseTracker.Infrastructure.Services;

// namespace ExpenseTracker.Tests.Infrastructure.Test.Category;

// public class GetAllTest
// {
//     private readonly TestConnectionService<ExpenseTracker.Core.Models.Category> _mongoDbService;
//     private readonly IMongoCollection<ExpenseTracker.Core.Models.Category> _mockCollection;

//     public GetAllTest()
//     {
//         // MongoDB bağlantısı için gerekli ayarları yapın
//         var connectionString = "mongodb+srv://new:PvtvAYVRDL8UiabA@expensetrackertestdb.v9x2c.mongodb.net/";
//         var databaseName = "ExpenseTrackerTestDb";
//         var collectionName = "Category";

//         var client = new MongoClient(connectionString);
//         var database = client.GetDatabase(databaseName);
//         _mockCollection = database.GetCollection<ExpenseTracker.Core.Models.Category>(collectionName);

//         // MongoDB servisini oluşturun
//         _mongoDbService = new TestConnectionService<ExpenseTracker.Core.Models.Category>(collectionName);
//     }

//     [Fact]
//     public async Task GetAllAsync_ReturnsAllItems()
//     {
//         // Arrange
//         var testExpense = new ExpenseTracker.Core.Models.Category
//         {
//             Id = new Guid(), // MongoDB ID'si için string kullanıyoruz
//             Name = "Sample Name",
//             Description = "Sample Description",
//         };

//         await _mockCollection.InsertOneAsync(testExpense);

//         // Act
//         var result = await _mongoDbService.GetAllAsync();

//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<List<ExpenseTracker.Core.Models.Category>>(result);
//         Assert.Single(result);
//         //Assert.Equal("Sample Description", result[0].Description);
//     }

//     [Fact]
//     public async Task GetAllAsync_ReturnsEmptyList_WhenNoDocuments()
//     {
//         // Arrange
//         // Koleksiyonu temizle
//         await _mockCollection.DeleteManyAsync(FilterDefinition<ExpenseTracker.Core.Models.Category>.Empty);

//         // Act
//         var result = await _mongoDbService.GetAllAsync();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Empty(result);
//     }
// }

