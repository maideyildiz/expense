namespace ExpenseTracker.API.Controllers;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Contracts.Authentication;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Core.Common.Errors;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this._mapper = mapper;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = this._mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = (ErrorOr<AuthenticationResult>)await mediator.Send(request);

        return authResult.Match(
            authResult => Ok(this._mapper.Map<AuthenticationResult>(authResult)),
            errors => Problem(errors));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = this._mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = (ErrorOr<AuthenticationResult>)await mediator.Send(request);
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(this._mapper.Map<AuthenticationResult>(authResult)),
            errors => Problem(errors));
    }
}

