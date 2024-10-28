namespace ExpenseTracker.API;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ExpenseTracker.API.Common.Errors;
using ExpenseTracker.API.Common.Mapping;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ExpenseTrackerProblemDetailsFactory>();
        return services;
    }
}