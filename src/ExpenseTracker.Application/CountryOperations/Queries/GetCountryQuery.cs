using ErrorOr;

using ExpenseTracker.Application.CountryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public record GetCountryQuery(Guid Id) : IRequest<ErrorOr<GetCountryResult>>;