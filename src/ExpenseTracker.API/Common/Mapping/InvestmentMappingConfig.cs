using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Contracts.InvestmentOperations;

using Mapster;

namespace ExpenseTracker.API.Common.Mapping;

public class InvestmentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateInvestmentRequest, CreateInvestmentCommand>();
        config.NewConfig<GetInvestmentsRequest, GetInvestmentsQuery>();
        config.NewConfig<GetInvestmentsResponse, InvestmentResult>();
        // config.NewConfig<bool, DeleteInvestmentResponse>()
        // .Map(dest => dest.Success, src => src);
        config.NewConfig<PagedResult<InvestmentResult>, GetInvestmentsResponse>()
           .Map(dest => dest.Items, src => src.Items.Adapt<List<GetInvestmentResponse>>())
           .Map(dest => dest.TotalCount, src => src.TotalCount)
           .Map(dest => dest.Page, src => src.Page)
           .Map(dest => dest.PageSize, src => src.PageSize);
    }
}