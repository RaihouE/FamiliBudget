using FamiliBudget.App.Application;
using FamiliBudget.App.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamiliBudget.App.Controllers;

[Authorize]
public class BudgetController : Controller
{
	private readonly BudgetApiClient _budgetApiClient;
	private readonly BudgetsViewModelBuilder _budgetsViewModelBuilder;

	public BudgetController(BudgetApiClient budgetApiClient, BudgetsViewModelBuilder budgetsViewModelBuilder)
	{
		_budgetApiClient = budgetApiClient;
		_budgetsViewModelBuilder = budgetsViewModelBuilder;
	}

	public async Task<IActionResult> Index()
	{
		var token = HttpContext.Session.GetString("Token");

		if (token is null)
		{
			return Unauthorized();
		}

		var budgets = await _budgetApiClient.GetUserBudgets(token);

		var viewModel = _budgetsViewModelBuilder.Build(budgets);

		return View(viewModel);
	}
}
