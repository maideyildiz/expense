namespace ExpenseTracker.Contracts.ExpenseOperations;
public record GetExpenseResponse(
    Guid Id,
    decimal Amount,
    DateTime CreatedAt,
    string Description,
    string CategoryName
);