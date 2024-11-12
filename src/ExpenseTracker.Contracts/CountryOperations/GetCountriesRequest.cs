namespace ExpenseTracker.Contracts.CountryOperations;

public record GetCountriesRequest(int Page = 1, int PageSize = 10);