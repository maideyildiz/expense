using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Infrastructure.Helpers;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ExpenseTracker.Application.UserOperations.Common;
using ExpenseTracker.Application.UserOperations.Commands;

namespace ExpenseTracker.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _baseRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IJwtTokenGenerator jwtTokenGenerator,
        IBaseRepository<User> baseRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _baseRepository = baseRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public ErrorOr<Guid> GetUserId()
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out Guid userId))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        return userId;
    }

    public async Task<ErrorOr<string>> LoginUserAsync(LoginQuery query)
    {
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        var user = await _baseRepository.GetByQueryAsync(sql, new { Email = query.Email });

        if (user is null || !user.IsActive || !PasswordHasher.VerifyPassword(query.Password, user.PasswordHash))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = this._jwtTokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command)
    {
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        var existingUser = await _baseRepository.GetByQueryAsync(sql, new { Email = command.Email });
        if (existingUser?.Id != null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var newUser = User.Create(command.FirstName, command.LastName, command.Email, passwordHash, command.CityId);

        await _baseRepository.AddAsync(newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);

        return token;
    }

    public async Task<ErrorOr<UserResult>> GetUserDetailsAsync(Guid userId)
    {
        var query = @"
            SELECT u.Id, u.FirstName, u.LastName, u.Email, u.MonthlySalary, u.YearlySalary, c.Name AS CityName
            FROM Users u
            LEFT JOIN Cities c ON e.CityId = c.Id
            WHERE e.Id = @Id";

        var userDetails = await _baseRepository.GetByQueryAsync(query, new { Id = userId });
        return Errors.Investment.InvestmentUpdateFailed;
    }
    public Task<ErrorOr<UserResult>> UpdateUserDetailsAsync(UpdateUserProfileCommand command, Guid userId)
    {
        throw new NotImplementedException();
    }
}