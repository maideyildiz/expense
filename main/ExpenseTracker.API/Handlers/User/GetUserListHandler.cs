using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.API.Requests.Queries;
using ExpenseTracker.Infrastructure.Abstractions;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers.User;

public class GetUserListHandler : IRequestHandler<GetUserListQuery, List<GetUserListQueryResult>>
{
    private readonly IUserService _userService;
    public GetUserListHandler(IUserService userService)
    {
        this._userService = userService;
    }
    public async Task<List<GetUserListQueryResult>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync();
        return users.Adapt<List<GetUserListQueryResult>>();
    }
}