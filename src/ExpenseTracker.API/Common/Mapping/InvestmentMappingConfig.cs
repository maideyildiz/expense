using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Contracts.InvestmentOperations;

using Mapster;

namespace ExpenseTracker.API.Common.Mapping;

public class InvestmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateInvestmentRequest, CreateInvestmentCommand>();
    }
}