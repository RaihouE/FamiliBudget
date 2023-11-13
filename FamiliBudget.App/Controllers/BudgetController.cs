using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamiliBudget.App.Controllers;

[Authorize]
public class BudgetController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
