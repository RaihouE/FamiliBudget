using FamiliBudget.App.Infrastructure;
using FamiliBudget.App.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamiliBudget.App.Controllers;

[AllowAnonymous]
public class UserController : Controller
{
	private readonly ILogger<UserController> _logger;
	private readonly BudgetApiClient _budgetApiClient;

	public UserController(ILogger<UserController> logger, BudgetApiClient budgetApiClient)
	{
		_logger = logger;
		_budgetApiClient = budgetApiClient;
	}

	[HttpGet]
	public async Task<IActionResult> Login()
	{
		return View(new LoginModel());
	}


	[HttpPost]
	[AllowAnonymous]
	public async Task<IActionResult> Login(string username, string password)
	{
		try
		{
			var tokenResult = await _budgetApiClient.Login(username, password);

			if (tokenResult is { Token: not null })
			{
				HttpContext.Session.SetString("Token", tokenResult.Token);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.ToString());
		}

		return RedirectToAction("index", "budget");
	}
}
