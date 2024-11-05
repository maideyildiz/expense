namespace ExpenseTracker.Application.ExpenseOperations.Queries;

public record GetExpensesQueryResult(
    Guid Id,
    decimal Amount,
    DateTime CreatedAt,
    string Description,
    string CategoryName);