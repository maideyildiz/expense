using ExpenseTracker.Contracts.Common;

namespace ExpenseTracker.Contracts.ExpenseOperations;
public record GetExpensesResponse(
    List<GetExpenseResponse> Items,
    int TotalCount,
    int Page,
    int PageSize
);