using FamiliBudget.App.Infrastructure;

namespace FamiliBudget.App.Application;

public class BudgetsViewModelBuilder
{
	public List<BudgetViewModel> Build(BudgetResponse? data)
	{
		if (data is null)
		{
			return new List<BudgetViewModel> { };
		}

		var budgets = data.Budgets
			.Select(b => new BudgetViewModel
			{
				Id = b.Id,
				Name = b.Name,
				Income = b.Income,
				Expenses = b.Expenses
					.Select(e => new ExpenseViewModel
					{
						Id = e.Id,
						Type = e.Type,
						Value = e.Value,
					})
					.ToList(),
			})
			.ToList();

		return budgets;
	}
}
