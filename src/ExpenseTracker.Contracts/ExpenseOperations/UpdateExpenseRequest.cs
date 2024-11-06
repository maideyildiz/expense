namespace ExpenseTracker.Contracts.ExpenseOperations;

public record UpdateExpenseRequest(
    decimal Amount,
    string Description,
    Guid CategoryId
);