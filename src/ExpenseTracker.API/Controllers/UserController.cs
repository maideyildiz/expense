using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Contracts.ExpenseOperations;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.UserOperations.Queries;
using ExpenseTracker.Contracts.UserOperations;
using ExpenseTracker.Application.UserOperations.Commands.Update;
using ExpenseTracker.Application.UserOperations.Commands.Logout;

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
        var query = new GetUserProfileQuery();
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<UserResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserRequest request)
    {
        var command = _mapper.Map<UpdateUserProfileCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(_mapper.Map<UserResponse>(result.Value)),
            errors => Problem(statusCode: (int)errors.First().Type, detail: errors.First().Description));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var query = new LogoutCommand();
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(result.Value),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }
}