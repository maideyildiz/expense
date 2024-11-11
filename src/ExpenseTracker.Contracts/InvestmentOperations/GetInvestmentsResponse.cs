using ExpenseTracker.Contracts.Common;

namespace ExpenseTracker.Contracts.InvestmentOperations;
public record GetInvestmentsResponse(
    List<GetInvestmentResponse> Items,
    int TotalCount,
    int Page,
    int PageSize
);