using ErrorOr;

using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public record GetInvestmentsQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<InvestmentResult>>>;