using ExpenseTracker.Application.UserOperations.Common;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.UserOperations.Queries;

public record GetUserProfileQuery : IRequest<ErrorOr<UserResult>>;