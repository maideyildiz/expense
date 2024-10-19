using ExpenseTracker.API.DTOs;
using MediatR;

namespace ExpenseTracker.API.Requests.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdQueryResult>;