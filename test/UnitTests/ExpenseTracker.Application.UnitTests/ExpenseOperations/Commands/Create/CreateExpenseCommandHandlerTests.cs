using Xunit;
using Moq;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
namespace ExpenseTracker.Application.UnitTests.ExpenseOperations.Commands.Create;
public class CreateExpenseCommandHandlerTests
{
    [Fact]
    public async Task HandleCreateExpenseCommandHandler_ShouldReturnExpenseResult_WhenExpenseIsCreatedSuccessfully()
    {
        // Arrange
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        var userId = Guid.NewGuid();

        mockUserService
            .Setup(service => service.GetUserId())
            .Returns(userId);

        var createExpenseCommand = new CreateExpenseCommand(100.50m, "Test Expense", Guid.NewGuid());

        var expenseId = Guid.NewGuid();

        mockExpenseService
            .Setup(service => service.AddExpenseAsync(createExpenseCommand, userId))
            .ReturnsAsync(expenseId);

        var expenseResult = new ExpenseResult(expenseId, createExpenseCommand.Amount, createExpenseCommand.Description, DateTime.UtcNow, "Test Category", userId);

        mockExpenseService
            .Setup(service => service.GetExpenseByIdAsync(expenseId))
            .ReturnsAsync(expenseResult);

        var handler = new CreateExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(createExpenseCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "The result should not contain an error.");
        Assert.Equal(expenseResult.Id, result.Value.Id);
        Assert.Equal(expenseResult.Amount, result.Value.Amount);
        Assert.Equal(expenseResult.CategoryName, result.Value.CategoryName);
        Assert.Equal(expenseResult.Description, result.Value.Description);
        Assert.Equal(expenseResult.UpdatedAt, result.Value.UpdatedAt);

        mockUserService.Verify(service => service.GetUserId(), Times.Once, "GetUserId should be called once.");
        mockExpenseService.Verify(
            service => service.AddExpenseAsync(createExpenseCommand, userId),
            Times.Once,
            "AddExpenseAsync should be called once with the correct parameters.");
        mockExpenseService.Verify(
            service => service.GetExpenseByIdAsync(expenseId),
            Times.Once,
            "GetExpenseByIdAsync should be called once with the correct expense ID.");
    }

    [Fact]
    public async Task HandleCreateExpenseCommandHandler_ShouldReturnError_WhenExpenseIsNotCreated()
    {
        // Arrange
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        var userId = Guid.NewGuid();

        mockUserService
            .Setup(service => service.GetUserId())
            .Returns(userId);

        // Example create expense command
        var createExpenseCommand = new CreateExpenseCommand(100.50m, "Test Expense", Guid.NewGuid());

        mockExpenseService
            .Setup(service => service.AddExpenseAsync(createExpenseCommand, userId))
            .ReturnsAsync(Error.Failure("Expense creation failed"));

        var handler = new CreateExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(createExpenseCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "The result should contain an error.");
        Assert.Contains(result.Errors, error => error.Code == "Expense.ExpenseCreationFailed");

        mockUserService.Verify(service => service.GetUserId(), Times.Once, "GetUserId should be called once.");
        mockExpenseService.Verify(
            service => service.AddExpenseAsync(createExpenseCommand, userId),
            Times.Once,
            "AddExpenseAsync should be called once with the correct parameters.");
        mockExpenseService.Verify(
            service => service.GetExpenseByIdAsync(It.IsAny<Guid>()),
            Times.Never,
            "GetExpenseByIdAsync should not be called if expense creation failed.");
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserIdCannotBeRetrieved()
    {
        // Arrange
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(service => service.GetUserId())
            .Returns(Error.Validation("Auth.InvalidCredentials", "The operation failed. Please check your information and try again."));

        var createExpenseCommand = new CreateExpenseCommand(100.50m, "Test Expense", Guid.NewGuid());

        var handler = new CreateExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(createExpenseCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "The result should contain an error.");
        Assert.Contains(result.Errors, error => error.Code == "Auth.InvalidCredentials");

        mockUserService.Verify(service => service.GetUserId(), Times.Once, "GetUserId should be called once.");
        mockExpenseService.Verify(
            service => service.AddExpenseAsync(It.IsAny<CreateExpenseCommand>(), It.IsAny<Guid>()),
            Times.Never,
            "AddExpenseAsync should not be called if user ID retrieval fails.");
        mockExpenseService.Verify(
            service => service.GetExpenseByIdAsync(It.IsAny<Guid>()),
            Times.Never,
            "GetExpenseByIdAsync should not be called if user ID retrieval fails.");
    }


}
