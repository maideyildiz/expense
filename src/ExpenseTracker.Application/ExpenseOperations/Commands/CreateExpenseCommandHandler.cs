using System.Security.Claims;
using ExpenseTracker.Core.Common.Errors;
using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.ExpenseAggregate;
using ExpenseTracker.Core.ExpenseAggregate.ValueObjests;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Core.Common.Entities;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ErrorOr<int>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Expense> _expenseRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Category> _categoryRepository;

    public CreateExpenseCommandHandler(
        IHttpContextAccessor httpContextAccessor,
        IBaseRepository<Expense> expenseRepository,
        IBaseRepository<User> userRepository,
        IBaseRepository<Category> categoryRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var userIdGuid = Guid.Parse(userId);
        var user = await _userRepository.GetByIdAsync(userIdGuid);
        if (user == null)
        {
            return Errors.Expense.ExpenseCreationFailed;
        }
        var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
        if (category == null)
        {
            return Errors.Expense.ExpenseCreationFailed;
        }

        Expense expense = Expense.Create(
            ExpenseId.CreateUnique(),
            command.Amount,
            DateTime.UtcNow,
            DateTime.UtcNow,
            command.Description,
            category.Id,
            user.Id);

        var result = await _expenseRepository.AddAsync(expense);

        return result > 0 ? result : Errors.Expense.ExpenseCreationFailed;
    }
}