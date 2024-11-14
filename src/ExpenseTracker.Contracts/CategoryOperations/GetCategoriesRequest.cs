namespace ExpenseTracker.Contracts.CategoryOperations;

public record GetCategoriesRequest(int Page = 1, int PageSize = 10);