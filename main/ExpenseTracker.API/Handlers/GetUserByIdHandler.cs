using ExpenseTracker.API.DTOs;
using ExpenseTracker.API.Requests.Queries;
using ExpenseTracker.Infrastructure.Abstractions;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
{
    private readonly IUserService _userService;
    public GetUserByIdHandler(IUserService userService)
    {
        this._userService = userService;
    }

    public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id);
        return user.Adapt<GetUserByIdQueryResult>();
    }
}