using ExpenseTracker.API.DTOs;
using MediatR;

namespace ExpenseTracker.API.Requests.Queries;

public record GetUserListQuery() : IRequest<List<GetUserListQueryResult>>;