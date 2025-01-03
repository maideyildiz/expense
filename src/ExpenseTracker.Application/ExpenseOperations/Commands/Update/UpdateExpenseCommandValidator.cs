using ExpenseTracker.Application.ExpenseOperations.Commands.Create;

using FluentValidation;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Update;

public class UpdateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
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