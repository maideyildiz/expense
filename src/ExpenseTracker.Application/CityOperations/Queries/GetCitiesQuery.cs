using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public record GetCitiesQuery(Guid CountryId, int Page, int PageSize) : IRequest<ErrorOr<PagedResult<GetCityResult>>>;