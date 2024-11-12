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
using ExpenseTracker.Application.UserOperations.Commands.Update;

namespace ExpenseTracker.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
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
        var user = await _userRepository.GetByQueryAsync(sql, new { Email = query.Email });

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
        var existingUser = await _userRepository.GetByQueryAsync(sql, new { Email = command.Email });
        if (existingUser?.Id != null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var newUser = User.Create(command.FirstName, command.LastName, command.Email, passwordHash, command.CityId);

        await _userRepository.AddAsync(newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);

        return token;
    }

    public async Task<ErrorOr<UserResult>> GetUserDetailsAsync(Guid userId)
    {
        var userDetails = await _userRepository.GetUserDetailsAsync(userId);
        if (userDetails == null)
        {
            return Errors.User.UserNotFound;
        }

        return userDetails;
    }
    public async Task<ErrorOr<UserResult>> UpdateUserDetailsAsync(UpdateUserProfileCommand command, Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Errors.User.UserNotFound;
        }
        string psw = command.Password == null ? user.PasswordHash : PasswordHasher.HashPassword(command.Password);
        user.Update(
            command.FirstName,
            command.LastName,
            command.Email,
            psw,
            command.CityId,
            command.MonthlySalary,
            command.YearlySalary,
            command.IsActive);

        if (await _userRepository.UpdateAsync(user) > 0)
        {
            return await _userRepository.GetUserDetailsAsync(userId);
        }
        else
        {
            return Errors.User.UserUpdateFailed;
        }
    }

    public async Task<ErrorOr<bool>> LogoutUserAsync(Guid userId)
    {
        return true;
    }
}