namespace ExpenseTracker.Contracts.CityOperations;

public record GetCitiesRequest(Guid CountryId, int Page = 1, int PageSize = 10);