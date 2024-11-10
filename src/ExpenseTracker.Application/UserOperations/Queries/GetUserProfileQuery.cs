using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.UserOperations.Queries;

public record GetUserProfileQuery : IRequest<ErrorOr<int>>;