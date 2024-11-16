using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Commands.Update;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Core.Entities;
using ErrorOr;

namespace ExpenseTracker.Infrastructure.Services;

public class InvestmentService : IInvestmentService
{
    private readonly ICacheService _redisCacheService;
    private readonly IInvestmentRepository _investmentRepository;
    private string GetInvestmentsCacheKey(Guid userId) => $"GetInvestmentsAsync_{userId}";
    private string GetInvestmentByIdCacheKey(Guid id) => $"GetInvestmentByIdAsync_{id}";

    public InvestmentService(
        ICacheService redisCacheService,
        IInvestmentRepository investmentRepository)
    {
        _redisCacheService = redisCacheService;
        _investmentRepository = investmentRepository;
    }

    public async Task<ErrorOr<Guid>> AddInvestmentAsync(CreateInvestmentCommand command, Guid userId)
    {
        Investment investment = Investment.Create(
            command.Amount,
            command.Description,
            userId,
            command.CategoryId);

        var result = await _investmentRepository.AddAsync(investment);
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
        Guid investmentUserId = await _investmentRepository.GetInvestmentUserIdAsync(investmentId);
        if (investmentUserId == Guid.Empty || investmentUserId != userId)
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
        Investment investment = await _investmentRepository.GetByIdAsync(id);
        if (investment == null)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        var result = await _investmentRepository.DeleteAsync(id);
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
        var investment = await _investmentRepository.GetInvestmentByIdAsync(id);
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
        var investments = await _investmentRepository.GetInvestmentsByUserIdAsync(userId, page, pageSize);
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
            investment = await _investmentRepository.GetByIdAsync(command.Id);
        }
        if (investment == null)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        investment.Update(command.Amount, command.Description, command.CategoryId);
        if (await _investmentRepository.UpdateAsync(investment) > 0)
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