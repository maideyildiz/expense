using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Services;
using Moq;

namespace ExpenseTracker.Tests.Infrastructure.Test.Expense;

public class GetAllTest
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        // Arrange
        var mockCollectionReference = new Mock<CollectionReference>();
        var mockQuerySnapshot = new Mock<QuerySnapshot>();
        var mockDocumentSnapshot = new Mock<DocumentSnapshot>();

        mockCollectionReference.Setup(c => c.GetSnapshotAsync()).ReturnsAsync(mockQuerySnapshot.Object);
        mockQuerySnapshot.Setup(q => q.Documents).Returns(new[] { mockDocumentSnapshot.Object });
        mockDocumentSnapshot.Setup(d => d.ConvertTo<ExpenseTracker.Core.Models.Expense>()).Returns(new ExpenseTracker.Core.Models.Expense());

        var baseService = new BaseService(mockCollectionReference.Object);

        // Act
        var result = await baseService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ExpenseTracker.Core.Models.Expense>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoDocuments()
    {
        // Arrange
        var mockCollectionReference = new Mock<CollectionReference>();
        var mockQuerySnapshot = new Mock<QuerySnapshot>();

        mockCollectionReference.Setup(c => c.GetSnapshotAsync()).ReturnsAsync(mockQuerySnapshot.Object);
        mockQuerySnapshot.Setup(q => q.Documents).Returns(new List<DocumentSnapshot>());

        var baseService = new BaseService(mockCollectionReference.Object);

        // Act
        var result = await baseService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
