using FamiliBudget.Api.Application;
using FamiliBudget.Api.Application.Models;

namespace FamiliBudget.Api;

public static class Services
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<BudgetService>();
        services.AddTransient<GetBudgetResponseBuilder>();
    }
}
