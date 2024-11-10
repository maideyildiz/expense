using ExpenseTracker.Application.ExpenseOperations.Commands.Create;

using FluentValidation;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Create;

public class CreateInvestmentCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateInvestmentCommandValidator()
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