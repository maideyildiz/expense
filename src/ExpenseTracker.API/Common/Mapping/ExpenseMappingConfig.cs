using Mapster;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Contracts.Common;
using ExpenseTracker.Contracts.ExpenseOperations;

namespace ExpenseTracker.API.Common.Mapping;
public class ExpenseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateExpenseRequest, CreateExpenseCommand>();
        config.NewConfig<GetExpenseResponse, ExpenseResult>();
        config.NewConfig<GetExpensesRequest, GetExpensesQuery>();
        config.NewConfig<PagedResult<ExpenseResult>, GetExpensesResponse>()
            .Map(dest => dest.Items, src => src.Items.Adapt<List<GetExpenseResponse>>())
            .Map(dest => dest.TotalCount, src => src.TotalCount)
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize);
    }
}