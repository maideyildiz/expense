using ExpenseTracker.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var token = await _mediator.Send(request);
            return Ok(token);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }
    }
}
