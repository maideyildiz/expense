using FluentValidation;

namespace ExpenseTracker.Application.CityOperations.Queries;

public class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
{
    public GetCitiesQueryValidator()
    {
        RuleFor(c => c.CountryId)
        .NotEmpty()
        .NotNull();
    }
}