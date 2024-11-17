using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.CategoryOperations.Queries;
using ExpenseTracker.Application.Common.Interfaces.Services;

using Moq;

namespace ExpenseTracker.UnitTests.Application.UnitTests.CategoryOperations.Queries;


public class GetInvestmentCategoryQueryHandlerTests
{
    [Fact]
    public async Task HandleGetInvestmentCategoryQuery_WhenInvestmentCategoryExists_ShouldReturnInvestmentCategory()
    {
        var investmentCategoryId = Guid.NewGuid();
        var expectedInvestmentCategory = new CategoryResult(investmentCategoryId, "TestInvestmentCategory");

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
            .Setup(service => service.GetInvestmentCategoryByIdAsync(investmentCategoryId))
            .ReturnsAsync(expectedInvestmentCategory);

        var handler = new GetInvestmentCategoryQueryHandler(mockCategoryService.Object);
        var query = new GetCategoryQuery(investmentCategoryId);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Equal(expectedInvestmentCategory.Id, result.Value.Id);
        Assert.Equal(expectedInvestmentCategory.Name, result.Value.Name);

        mockCategoryService.Verify(
            service => service.GetInvestmentCategoryByIdAsync(investmentCategoryId),
            Times.Once,
            "GetInvestmentCategoryByIdAsync should be called once with the correct investment category ID.");

    }
    [Fact]
    public async Task HandleGetInvestmentCategoryQuery_WhenInvestmentCategoryDoesNotExist_ShouldReturnError()
    {
        var investmentCategoryId = Guid.NewGuid();
        var expectedError = Error.NotFound("InvestmentCategory.NotFound", "NotFound");

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService
                    .Setup(service => service.GetInvestmentCategoryByIdAsync(investmentCategoryId))
                    .ReturnsAsync(expectedError);

        var handler = new GetInvestmentCategoryQueryHandler(mockCategoryService.Object);
        var query = new GetCategoryQuery(investmentCategoryId);

        var result = await handler.Handle(query, CancellationToken.None);
        Assert.True(result.IsError, "Result should contain an error.");
        Assert.Equal(expectedError, result.FirstError);
        mockCategoryService.Verify(
                    service => service.GetInvestmentCategoryByIdAsync(investmentCategoryId),
                    Times.Once,
                    "GetInvestmentCategoryByIdAsync should be called once with the correct investment category ID.");
    }
}