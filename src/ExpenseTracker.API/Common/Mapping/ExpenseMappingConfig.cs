
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Contracts.ExpenseOperations;

using Mapster;
namespace ExpenseTracker.API.Common.Mapping;
public class ExpenseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateExpenseRequest, CreateExpenseCommand>();

    }
}