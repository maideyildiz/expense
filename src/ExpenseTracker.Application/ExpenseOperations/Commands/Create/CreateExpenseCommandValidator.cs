using FluentValidation;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Create;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
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