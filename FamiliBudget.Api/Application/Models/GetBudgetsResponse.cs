namespace FamiliBudget.Api.Application.Models;

public record GetBudgetsResponse(List<GetBudgetsResponse.Budget> Budgets)
{
    public record Budget(int Id, string Name, decimal Income, List<Budget.Expense> Expenses)
    {
        public record Expense(int Id, string Type, decimal Value);
    }
}
