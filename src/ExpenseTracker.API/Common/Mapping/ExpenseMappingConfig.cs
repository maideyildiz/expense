
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Contracts.Common;
using ExpenseTracker.Contracts.ExpenseOperations;

using Mapster;
namespace ExpenseTracker.API.Common.Mapping;
public class ExpenseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateExpenseRequest, CreateExpenseCommand>();
        config.NewConfig<GetExpenseResponse, GetExpenseQueryResult>();
        config.NewConfig<GetExpensesRequest, GetExpensesQuery>();
        config.NewConfig<PagedResult<GetExpenseQueryResult>, GetExpensesResponse>()
            .Map(dest => dest.Items, src => src.Items.Adapt<List<GetExpenseResponse>>())
            .Map(dest => dest.TotalCount, src => src.TotalCount)
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize);



    }
}