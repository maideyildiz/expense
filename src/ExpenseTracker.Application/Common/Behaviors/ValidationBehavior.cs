namespace ExpenseTracker.Application.Common.Behaviors;
using MediatR;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>>? validators = null)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators is not null && _validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

            if (failures.Count != 0)
            {
                //return TryCreateErrorResponseFromErrors(failures, out var errorResponse) ? errorResponse : throw new ValidationException(failures);
                var errors = failures.ConvertAll(f => Error.Validation(f.PropertyName, f.ErrorMessage));
                return (dynamic)errors;
            }
        }

        return await next();
    }

    private static bool TryCreateErrorResponseFromErrors(List<ValidationFailure> validationFailures, out TResponse errorResponse)
    {
        List<Error> errors = validationFailures
            .ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

        try
        {
            errorResponse = (TResponse)(dynamic)errors;
            return true;
        }
        catch
        {
            errorResponse = default!;
            return false;
        }
    }
}
