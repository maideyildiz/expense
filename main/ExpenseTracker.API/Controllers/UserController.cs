using ExpenseTracker.API.Requests.Commands.User;
using ExpenseTracker.API.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertUserCommand request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserCommand request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteUserCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }
    }
}
