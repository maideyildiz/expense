
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, ErrorOr<GetCountryResult>>
{
    private readonly ICountryService _countryService;

    public GetCountryQueryHandler(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<ErrorOr<GetCountryResult>> Handle(GetCountryQuery query, CancellationToken cancellationToken)
    {
        return await _countryService.GetCountryByIdAsync(query.Id);
    }
}
