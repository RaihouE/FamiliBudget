namespace FamiliBudget.App.Application;

public class BudgetViewModel
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public decimal Income { get; set; }
	public List<ExpenseViewModel> Expenses { get; set; } = new List<ExpenseViewModel>();
}
