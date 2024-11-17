using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.CategoryOperations.Queries;
using ExpenseTracker.Application.Common.Interfaces.Services;

using Moq;

namespace ExpenseTracker.UnitTests.Application.UnitTests.CategoryOperations.Queries;


public class GetExpenseCategoryQueryHandlerTests
{
    [Fact]
    public async Task HandleGetExpenseCategoryQuery_WhenExpenseCategoryExists_ShouldReturnExpenseCategory()
    {
        var expenseCategoryId = Guid.NewGuid();
        var expectedExpenseCategory = new CategoryResult(expenseCategoryId, "TestExpenseCategory");

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
            .Setup(service => service.GetExpenseCategoryByIdAsync(expenseCategoryId))
            .ReturnsAsync(expectedExpenseCategory);

        var handler = new GetExpenseCategoryQueryHandler(mockCategoryService.Object);
        var query = new GetCategoryQuery(expenseCategoryId);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Equal(expectedExpenseCategory.Id, result.Value.Id);
        Assert.Equal(expectedExpenseCategory.Name, result.Value.Name);

        mockCategoryService.Verify(
            service => service.GetExpenseCategoryByIdAsync(expenseCategoryId),
            Times.Once,
            "GetExpenseCategoryByIdAsync should be called once with the correct expense category ID.");

    }
    [Fact]
    public async Task HandleGetExpenseCategoryQuery_WhenExpenseCategoryDoesNotExist_ShouldReturnError()
    {
        var expenseCategoryId = Guid.NewGuid();
        var expectedError = Error.NotFound("ExpenseCategory.NotFound", "NotFound");

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
                    .Setup(service => service.GetExpenseCategoryByIdAsync(expenseCategoryId))
                    .ReturnsAsync(expectedError);

        var handler = new GetExpenseCategoryQueryHandler(mockCategoryService.Object);
        var query = new GetCategoryQuery(expenseCategoryId);

        var result = await handler.Handle(query, CancellationToken.None);
        Assert.True(result.IsError, "Result should contain an error.");
        Assert.Equal(expectedError, result.FirstError);
        mockCategoryService.Verify(
                    service => service.GetExpenseCategoryByIdAsync(expenseCategoryId),
                    Times.Once,
                    "GetExpenseCategoryByIdAsync should be called once with the correct expense category ID.");
    }
}