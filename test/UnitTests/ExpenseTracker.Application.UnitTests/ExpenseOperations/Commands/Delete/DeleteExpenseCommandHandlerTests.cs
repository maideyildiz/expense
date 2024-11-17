using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands.Delete;
using ExpenseTracker.Application.Common.Errors;
using Moq;

namespace ExpenseTracker.Application.UnitTests.ExpenseOperations.Commands.Delete;


public class DeleteExpenseCommandHandlerTests
{
    [Fact]
    public async Task HandleDeleteExpenseCommand_ShouldReturnTrue_WhenExpenseDeletedSuccessfully()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var userId = Guid.NewGuid();  // Simulate a user
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        // Mock GetUserId to return a valid user ID
        mockUserService.Setup(service => service.GetUserId()).Returns(userId);

        // Mock the CheckIfUserOwnsExpense to return true (user owns the expense)
        mockExpenseService.Setup(service => service.CheckIfUserOwnsExpense(userId, expenseId)).ReturnsAsync(true);

        // Mock DeleteExpenseAsync to return true wrapped in ErrorOr
        mockExpenseService.Setup(service => service.DeleteExpenseAsync(expenseId)).ReturnsAsync(true);

        // Create handler
        var handler = new DeleteExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(new DeleteExpenseCommand(expenseId), CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "The result should not contain an error.");  // No error should occur
        Assert.True(result.Value, "The expense should be successfully deleted.");  // The deletion should return true

        // Verify DeleteExpenseAsync and CheckIfUserOwnsExpense were called once
        mockExpenseService.Verify(service => service.CheckIfUserOwnsExpense(userId, expenseId), Times.Once);
        mockExpenseService.Verify(service => service.DeleteExpenseAsync(expenseId), Times.Once);
        mockUserService.Verify(service => service.GetUserId(), Times.Once);
    }

    [Fact]
    public async Task HandleDeleteExpenseCommand_ShouldReturnError_WhenUserDoesNotOwnExpense()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var userId = Guid.NewGuid();  // Simulate a user
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        // Mock GetUserId to return a valid user ID
        mockUserService.Setup(service => service.GetUserId()).Returns(userId);

        // Mock CheckIfUserOwnsExpense to return false (user does not own the expense)
        mockExpenseService.Setup(service => service.CheckIfUserOwnsExpense(userId, expenseId)).ReturnsAsync(false);

        // Create handler
        var handler = new DeleteExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(new DeleteExpenseCommand(expenseId), CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "The result should contain an error.");
        Assert.Equal(Errors.Expense.ExpenseNotFound, result.FirstError);  // Ensure correct error

        // Verify DeleteExpenseAsync was not called
        mockExpenseService.Verify(service => service.CheckIfUserOwnsExpense(userId, expenseId), Times.Once);
        mockExpenseService.Verify(service => service.DeleteExpenseAsync(expenseId), Times.Never);
        mockUserService.Verify(service => service.GetUserId(), Times.Once);
    }

    [Fact]
    public async Task HandleDeleteExpenseCommand_ShouldReturnError_WhenUserIdCannotBeRetrieved()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var mockExpenseService = new Mock<IExpenseService>();
        var mockUserService = new Mock<IUserService>();

        // Mock GetUserId to return an error (simulating a failure to retrieve user ID)
        mockUserService.Setup(service => service.GetUserId()).Returns(Errors.Authentication.InvalidCredentials);

        // Create handler
        var handler = new DeleteExpenseCommandHandler(mockExpenseService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(new DeleteExpenseCommand(expenseId), CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "The result should contain an error.");
        Assert.Equal(Errors.Authentication.InvalidCredentials, result.FirstError);  // Ensure correct error

        // Verify DeleteExpenseAsync was not called
        mockUserService.Verify(service => service.GetUserId(), Times.Once);
        mockExpenseService.Verify(service => service.DeleteExpenseAsync(expenseId), Times.Never);
    }


}