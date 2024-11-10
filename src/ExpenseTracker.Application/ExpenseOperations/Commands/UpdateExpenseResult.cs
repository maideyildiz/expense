namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public record UpdateExpenseResult(
    decimal Amount,
    string Description,
    DateTime UpdatedAt,
    string CategoryName);