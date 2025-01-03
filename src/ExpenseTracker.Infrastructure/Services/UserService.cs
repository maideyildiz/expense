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
using ExpenseTracker.Application.Common.Interfaces.Cache;

namespace ExpenseTracker.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IDbRepository _dbRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService _redisCacheService;
    private string GetUserByEmailCacheKey(string email) => $"GetUserByEmailAsync_{email}";
    private string GetUserByIdCacheKey(Guid id) => $"GetUserByIdAsync_{id}";
    private string GetUserDetailsByIdCacheKey(Guid id) => $"GetUserDetailsByIdAsync_{id}";

    public UserService(
        IDbRepository dbRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IHttpContextAccessor httpContextAccessor,
        ICacheService redisCacheService)
    {
        _dbRepository = dbRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _httpContextAccessor = httpContextAccessor;
        _redisCacheService = redisCacheService;
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
        string token = string.Empty;
        var cacheKey = GetUserByEmailCacheKey(query.Email);
        var cachedData = await _redisCacheService.GetAsync<User>(cacheKey);
        if (cachedData != null)
        {
            token = this._jwtTokenGenerator.GenerateToken(cachedData);
        }

        var sql = "SELECT * FROM Users WHERE Email = @Email";
        var user = await _dbRepository.QueryFirstOrDefaultAsync<User>(sql, new { Email = query.Email });

        if (user is null || !user.IsActive || !PasswordHasher.VerifyPassword(query.Password, user.PasswordHash))
        {
            return Errors.Authentication.InvalidCredentials;
        }
        await _redisCacheService.SetAsync(cacheKey, user);
        token = this._jwtTokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command)
    {
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        var existingUser = await _dbRepository.QueryFirstOrDefaultAsync<User>(sql, new { Email = command.Email });
        if (existingUser?.Id != null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var newUser = User.Create(command.FirstName, command.LastName, command.Email, command.Username, passwordHash, command.CityId);
        var insertClause = string.Join(", ", typeof(User).GetProperties().Select(p => p.Name));
        var valuesClause = string.Join(", ", typeof(User).GetProperties().Select(p => $"@{p.Name}"));
        string insertSql = $"INSERT INTO Users ({insertClause}) VALUES ({valuesClause})";
        await _dbRepository.ExecuteAsync(insertSql, newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);

        return token;
    }

    public async Task<ErrorOr<UserResult>> GetUserDetailsAsync(Guid userId)
    {
        var cacheKey = GetUserDetailsByIdCacheKey(userId);
        var cachedData = await _redisCacheService.GetAsync<UserResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        var query = @"
            SELECT u.Id, u.FirstName, u.LastName, u.Email, u.MonthlySalary, u.YearlySalary, c.Name AS CityName
            FROM Users u
            LEFT JOIN Cities c ON u.CityId = c.Id
            WHERE u.Id = @Id";
        var userDetails = await _dbRepository.QueryFirstOrDefaultAsync<UserResult>(query, new { UserId = userId });
        if (userDetails == null)
        {
            return Errors.User.UserNotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, userDetails);
        return userDetails;
    }
    public async Task<ErrorOr<UserResult>> UpdateUserDetailsAsync(UpdateUserProfileCommand command, Guid userId)
    {
        User user = null;
        var cacheKey = GetUserByIdCacheKey(userId);
        var cachedData = await _redisCacheService.GetAsync<User>(cacheKey);
        if (cachedData != null)
        {
            user = cachedData;
        }
        else
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            user = await _dbRepository.QueryFirstOrDefaultAsync<User>(sql, new { Email = userId });
        }
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

        var updateSql = @"
            UPDATE Users
            SET FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                PasswordHash = @PasswordHash,
                CityId = @CityId,
                MonthlySalary = @MonthlySalary,
                YearlySalary = @YearlySalary,
                IsActive = @IsActive
            WHERE Id = @Id";
        if (await _dbRepository.ExecuteAsync(updateSql, user) > 0)
        {
            await _redisCacheService.RemoveAsync(GetUserByEmailCacheKey(user.Email));
            await _redisCacheService.RemoveAsync(GetUserByIdCacheKey(userId));
            return await GetUserDetailsAsync(userId);
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