namespace ExpenseTracker.Application.ExpenseOperations.Queries;

public record GetExpenseQueryResult(
    Guid Id,
    decimal Amount,
    DateTime CreatedAt,
    string Description,
    string CategoryName,
    Guid UserId);