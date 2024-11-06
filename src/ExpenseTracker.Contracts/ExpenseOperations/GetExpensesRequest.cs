namespace ExpenseTracker.Contracts.ExpenseOperations;

public record GetExpensesRequest(int Page = 1, int PageSize = 10);