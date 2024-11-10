using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Contracts.Authentication;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Application.Common.Errors;
namespace ExpenseTracker.API.Controllers;
[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = this._mapper.Map<RegisterCommand>(request);
        ErrorOr<string> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(this._mapper.Map<string>(authResult)),
            errors => Problem(errors));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = this._mapper.Map<LoginQuery>(request);
        ErrorOr<string> authResult = await _mediator.Send(query);
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(this._mapper.Map<string>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok();
    }
}

