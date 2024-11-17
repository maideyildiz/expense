using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.CategoryOperations.Queries;
using ExpenseTracker.Application.Common.Interfaces.Services;

using Moq;

namespace ExpenseTracker.UnitTests.Application.UnitTests.CategoryOperations.Queries;

public class GetInvestmentCategoriesQueryHandlerTests
{
    [Fact]
    public async Task HandleGetInvestmentCategoriesQuery_WhenInvestmentCategoriesExist_ShouldReturnInvestmentCategories()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var totalCount = 25;
        var categories = new List<CategoryResult>
    {
        new CategoryResult (Guid.NewGuid(),  "Category1" ),
        new CategoryResult (Guid.NewGuid(),  "Category2" ),
        new CategoryResult (Guid.NewGuid(),  "Category3" ),
    };

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
            .Setup(service => service.GetInvestmentCategoriesAsync(page, pageSize))
            .ReturnsAsync((categories, totalCount));

        var handler = new GetInvestmentCategoriesQueryHandler(mockCategoryService.Object);
        var query = new GetCategoriesQuery(page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "Result should not contain an error.");
        Assert.Equal(categories.Count, result.Value.Items.Count);
        Assert.Equal(totalCount, result.Value.TotalCount);
        Assert.Equal(page, result.Value.Page);
        Assert.Equal(pageSize, result.Value.PageSize);

        mockCategoryService.Verify(
            service => service.GetInvestmentCategoriesAsync(page, pageSize),
            Times.Once,
            "GetInvestmentCategoriesAsync should be called once with the correct parameters.");
    }

    [Fact]
    public async Task HandleGetInvestmentCategoriesQuery_WhenInvestmentCategoriesDoNotExist_ShouldReturnEmptyList()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var totalCount = 0;
        var cities = new List<CategoryResult>();

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
            .Setup(service => service.GetInvestmentCategoriesAsync(page, pageSize))
            .ReturnsAsync((cities, totalCount));

        var handler = new GetInvestmentCategoriesQueryHandler(mockCategoryService.Object);
        var query = new GetCategoriesQuery(page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "Result should not contain an error.");
        Assert.Empty(result.Value.Items);
        Assert.Equal(totalCount, result.Value.TotalCount);
        Assert.Equal(page, result.Value.Page);
        Assert.Equal(pageSize, result.Value.PageSize);

        mockCategoryService.Verify(
            service => service.GetInvestmentCategoriesAsync(page, pageSize),
            Times.Once,
            "GetInvestmentCategoriesAsync should be called once with the correct parameters.");
    }
}