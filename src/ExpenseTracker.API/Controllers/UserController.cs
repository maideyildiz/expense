using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Contracts.ExpenseOperations;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;

namespace ExpenseTracker.API.Controllers;

[Route("user")]
public class UserController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        // var userId = GetUserIdFromClaim();

        // var query = new GetUserProfileQuery(userId);
        // var result = await _mediator.Send(query);

        return Ok();
    }


    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserProfile()
    {
        return Ok();
    }
}