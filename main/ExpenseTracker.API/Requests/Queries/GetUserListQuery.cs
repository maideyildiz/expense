using ExpenseTracker.API.DTOs.User;
using MediatR;

namespace ExpenseTracker.API.Requests.Queries;

public record GetUserListQuery() : IRequest<List<GetUserListQueryResult>>;