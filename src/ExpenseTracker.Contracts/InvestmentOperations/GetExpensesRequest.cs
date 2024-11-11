namespace ExpenseTracker.Contracts.InvestmentOperations;

public record GetInvestmentsRequest(int Page = 1, int PageSize = 10);