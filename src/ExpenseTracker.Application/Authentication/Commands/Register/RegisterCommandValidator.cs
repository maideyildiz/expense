namespace ExpenseTracker.Application.Authentication.Commands.Register;

using FluentValidation;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
        .NotEmpty()
            .MinimumLength(6);
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(10)
            .Matches(@"^(?!.*\b(firstname|lastname)\b).*").WithMessage("Username cannot contain 'firstname' or 'lastname'.");
    }
}