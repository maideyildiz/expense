namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public record UpdateExpenseResult(
    decimal Amount,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string CategoryName);