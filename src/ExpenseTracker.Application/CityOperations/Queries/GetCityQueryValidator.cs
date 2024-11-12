using FluentValidation;

namespace ExpenseTracker.Application.CityOperations.Queries;

public class GetCityQueryValidator : AbstractValidator<GetCityQuery>
{
    public GetCityQueryValidator()
    {
        RuleFor(c => c.Id)
        .NotEmpty()
        .NotNull();
    }
}