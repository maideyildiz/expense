using ExpenseTracker.Application.ExpenseOperations.Commands.Create;

using FluentValidation;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Update;

public class UpdateInvestmentCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public UpdateInvestmentCommandValidator()
    {
        RuleFor(c => c.Amount)
        .GreaterThan(0);
        RuleFor(c => c.Description)
        .NotEmpty()
        .NotNull();
        RuleFor(c => c.CategoryId)
        .NotEmpty()
        .NotNull();
    }
}