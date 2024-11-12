using ErrorOr;

using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.CountryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public record GetCountriesQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<GetCountryResult>>>;