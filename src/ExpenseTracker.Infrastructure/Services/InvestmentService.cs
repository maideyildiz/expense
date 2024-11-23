using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Commands.Update;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Core.Entities;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Cache;

namespace ExpenseTracker.Infrastructure.Services;

public class InvestmentService : IInvestmentService
{
    private readonly ICacheService _redisCacheService;
    private readonly IDbRepository _dbRepository;
    private string GetInvestmentsCacheKey(Guid userId) => $"GetInvestmentsAsync_{userId}";
    private string GetInvestmentByIdCacheKey(Guid id) => $"GetInvestmentByIdAsync_{id}";

    public InvestmentService(
        ICacheService redisCacheService,
        IDbRepository dbRepository)
    {
        _redisCacheService = redisCacheService;
        _dbRepository = dbRepository;
    }

    public async Task<ErrorOr<Guid>> AddInvestmentAsync(CreateInvestmentCommand command, Guid userId)
    {
        Investment investment = Investment.Create(
            command.Amount,
            command.Description,
            userId,
            command.CategoryId);
        string sql = "INSERT INTO Investments (Amount, Description, UserId, CategoryId) VALUES (@Amount, @Description, @UserId, @CategoryId)";
        var result = await _dbRepository.ExecuteAsync(sql, new { investment.Amount, investment.Description, investment.UserId, investment.CategoryId });
        if (result <= 0)
        {
            return Errors.Investment.InvestmentCreationFailed;
        }
        else
        {
            await _redisCacheService.RemoveAsync(GetInvestmentsCacheKey(investment.UserId));
            return investment.Id;
        }
    }

    public async Task<bool> CheckIfUserOwnsInvestment(Guid userId, Guid investmentId)
    {
        string sql = "SELECT Count(*) FROM Investments WHERE Id = @Id AND UserId = @UserId";
        int count = await _dbRepository.ExecuteScalarAsync<int>(sql, new { Id = investmentId, UserId = userId });
        if (count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<ErrorOr<bool>> DeleteInvestmentAsync(Guid id)
    {
        string sql = "SELECT * FROM Investments WHERE Id = @Id";
        Investment investment = await _dbRepository.QueryFirstOrDefaultAsync<Investment>(sql, new { Id = id });
        if (investment == null)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        string deleteSql = "DELETE FROM Investments WHERE Id = @Id";
        var result = await _dbRepository.ExecuteAsync(deleteSql, new { id = id });
        if (result <= 0)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        else
        {
            await _redisCacheService.RemoveAsync(GetInvestmentByIdCacheKey(investment.Id));
            await _redisCacheService.RemoveAsync(GetInvestmentsCacheKey(investment.UserId));
            return true;
        }
    }

    public async Task<ErrorOr<InvestmentResult>> GetInvestmentByIdAsync(Guid id)
    {
        string cacheKey = GetInvestmentByIdCacheKey(id);
        var cachedData = await _redisCacheService.GetAsync<InvestmentResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        string sql = @"
            SELECT e.Id, e.Amount, e.Description, e.UpdatedAt, c.Name AS CategoryName, e.UserId
            FROM Investments e
            LEFT JOIN InvestmentCategories c ON e.CategoryId = c.Id
            WHERE e.Id = @Id";
        var investment = await _dbRepository.QueryFirstOrDefaultAsync<InvestmentResult>(sql, new { Id = id });
        if (investment == null)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, investment);
        return investment;
    }

    public async Task<(IEnumerable<InvestmentResult> Items, int TotalCount)> GetInvestmentsAsync(Guid userId, int page, int pageSize)
    {
        var cacheKey = GetInvestmentsCacheKey(userId);
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<InvestmentResult>>(cacheKey);
        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }
        var query = @"
            SELECT i.Id, i.Amount, i.Description, i.UpdatedAt, c.Name AS CategoryName, i.UserId
            FROM Investments i
            LEFT JOIN InvestmentCategories c ON i.CategoryId = c.Id
            WHERE i.Id = @Id
            LIMIT @PageSize OFFSET @Offset";

        var investments = await _dbRepository.QueryAsync<InvestmentResult>(query, new { UserId = userId, PageSize = pageSize, Offset = (page - 1) * pageSize });
        await _redisCacheService.SetAsync(cacheKey, investments);
        return (investments, investments.Count());
    }

    public async Task<ErrorOr<InvestmentResult>> UpdateInvestmentAsync(UpdateInvestmentCommand command)
    {
        Investment investment = null;
        string cacheKey = GetInvestmentByIdCacheKey(command.Id);
        var cachedData = await _redisCacheService.GetAsync<Investment>(cacheKey);
        if (cachedData != null)
        {
            investment = cachedData;
        }
        else
        {
            string sql = "SELECT * FROM Investments WHERE Id = @Id";
            investment = await _dbRepository.QueryFirstOrDefaultAsync<Investment>(sql, new { Id = command.Id });
        }
        if (investment == null)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        investment.Update(command.Amount, command.Description, command.CategoryId);
        string updateSql = @"
            UPDATE Investments
            SET Amount = @Amount, Description = @Description, CategoryId = @CategoryId
            WHERE Id = @Id";
        if (await _dbRepository.ExecuteAsync(updateSql, investment) > 0)
        {
            await _redisCacheService.RemoveAsync(GetInvestmentByIdCacheKey(investment.Id));
            await _redisCacheService.RemoveAsync(GetInvestmentsCacheKey(investment.UserId));
            return await GetInvestmentByIdAsync(investment.Id);
        }
        else
        {
            return Errors.Investment.InvestmentNotFound;
        }
    }
}