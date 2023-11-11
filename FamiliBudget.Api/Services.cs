using FamiliBudget.Api.Application;

namespace FamiliBudget.Api;

public static class Services
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<BudgetService>();
    }
}
