using FamiliBudget.Api.Domain.Dictionaries;

namespace FamiliBudget.Api.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public ExpenseType Type { get; set; }
    public decimal Value { get; set; }
    public int BudgetId { get; set; }
    public required virtual Budget Budget { get; set; }
}
