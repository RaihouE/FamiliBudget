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
	public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
	{
		if (!ModelState.IsValid)
		{
			return View(loginModel);
		}

		try
		{
			var tokenResult = await _budgetApiClient.Login(loginModel.UserName!, loginModel.Password!);

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

	[HttpGet]
	public async Task<IActionResult> Register()
	{
		return View(new RegisterModel());
	}


	[HttpPost]
	public async Task<IActionResult> Register([FromForm] RegisterModel registerModel)
	{
		if (!ModelState.IsValid)
		{
			return View(registerModel);
		}

		try
		{
			var result = await _budgetApiClient.Register(registerModel.UserName!, registerModel.Password!, registerModel.Email!);

			if (result.Any())
			{
				ModelState.AddModelError("", string.Join(", ", result));
				return View(registerModel);
			}

			var tokenResult = await _budgetApiClient.Login(registerModel.UserName!, registerModel.Password!);

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
