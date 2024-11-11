using MediatR;
using ErrorOr;
namespace ExpenseTracker.Application.UserOperations.Commands;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ErrorOr<int>>
{
    public Task<ErrorOr<int>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
