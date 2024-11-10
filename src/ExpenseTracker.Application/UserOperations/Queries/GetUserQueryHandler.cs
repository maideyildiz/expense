
using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.UserOperations.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserProfileQuery, ErrorOr<int>>
{
    public Task<ErrorOr<int>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
