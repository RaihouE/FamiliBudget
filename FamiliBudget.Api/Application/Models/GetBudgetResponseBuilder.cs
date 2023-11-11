using FamiliBudget.Api.Domain.Entities;

namespace FamiliBudget.Api.Application.Models;

public class GetBudgetResponseBuilder
{
    public GetBudgetsResponse Build(IReadOnlyCollection<Budget> budgetsModels)
    {
        var budgets = new List<GetBudgetsResponse.Budget>();

        foreach (var budget in budgetsModels)
        {
            var expneses = new List<GetBudgetsResponse.Budget.Expense>();

            if (budget.Expenses is { Count: > 0 })
            {
                expneses.AddRange(budget.Expenses.Select(e => new GetBudgetsResponse.Budget.Expense(e.Id, e.Type.ToString(), e.Value)));
            }

            budgets.Add(new GetBudgetsResponse.Budget(budget.Id, budget.Name, budget.Income, expneses));
        }

        return new GetBudgetsResponse(budgets);
    }
}
