using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public record GetCityQuery(Guid Id) : IRequest<ErrorOr<GetCityResult>>;