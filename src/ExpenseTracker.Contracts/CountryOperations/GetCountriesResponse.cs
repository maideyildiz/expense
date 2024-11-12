using ExpenseTracker.Contracts.Common;

namespace ExpenseTracker.Contracts.CountryOperations;
public record GetCountriesResponse(
    List<GetCountryResponse> Items,
    int TotalCount,
    int Page,
    int PageSize
);