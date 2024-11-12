namespace ExpenseTracker.Contracts.CityOperations;

public record GetCityResponse(
    Guid Id,
    string Name
);