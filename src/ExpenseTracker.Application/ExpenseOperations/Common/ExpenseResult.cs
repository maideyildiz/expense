namespace ExpenseTracker.Application.ExpenseOperations.Commands.Common;

public record ExpenseResult(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime UpdatedAt,
    string CategoryName);