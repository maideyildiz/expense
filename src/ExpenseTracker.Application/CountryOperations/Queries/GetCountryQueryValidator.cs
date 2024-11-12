using FluentValidation;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public class GetCountryQueryValidator : AbstractValidator<GetCountryQuery>
{
    public GetCountryQueryValidator()
    {
        RuleFor(c => c.Id)
        .NotEmpty()
        .NotNull();
    }
}