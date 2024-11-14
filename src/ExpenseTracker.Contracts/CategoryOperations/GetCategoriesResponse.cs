namespace ExpenseTracker.Contracts.CategoryOperations;

public record GetCategoriesResponse(
    List<GetCategoryResponse> Items,
    int TotalCount,
    int Page,
    int PageSize);