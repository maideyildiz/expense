
using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ErrorOr<bool>>
{
    public Task<ErrorOr<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
