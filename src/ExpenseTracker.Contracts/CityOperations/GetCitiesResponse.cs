namespace ExpenseTracker.Contracts.CityOperations;

public record GetCitiesResponse(
    List<GetCityResponse> Items,
    int TotalCount,
    int Page,
    int PageSize
);