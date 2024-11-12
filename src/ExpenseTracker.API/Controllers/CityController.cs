using MapsterMapper;
using ExpenseTracker.Application.CityOperations.Queries;
using ExpenseTracker.Contracts.CityOperations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[Route("city")]
[AllowAnonymous]
public class CityController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CityController(ISender mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetCitiesRequest request)
    {
        var query = _mapper.Map<GetCitiesQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCitiesResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

}