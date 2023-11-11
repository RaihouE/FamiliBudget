using FamiliBudget.Api.Application;
using FamiliBudget.Api.Application.Models;
using FamiliBudget.Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamiliBudget.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly BudgetService _budgetService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly GetBudgetResponseBuilder _getBudgetResponseBuilder;

    public BudgetController(BudgetService budgetService, UserManager<IdentityUser> userManager, GetBudgetResponseBuilder getBudgetResponseBuilder)
    {
        _budgetService = budgetService;
        _userManager = userManager;
        _getBudgetResponseBuilder = getBudgetResponseBuilder;
    }

    [HttpPost]
    public async Task<ActionResult> AddBudget([FromBody] AddBudgetRequest request)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            return Unauthorized();
        }

        var expenses = new List<Expense>();

        if (request.Expenses is { Count: > 0 })
        {
            expenses.AddRange(request.Expenses.Select(e => new Expense { Value = e.Value, Type = ExpenseTypeMapper.Map(e.Type) }));
        }

        var newBudget = _budgetService.AddBudget(Guid.Parse(user.Id), request.Name, request.Income, expenses);

        return Ok(newBudget);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(IEnumerable<GetBudgetsResponse>), 200)]
    public ActionResult GetBudget(int id)
    {
        var budget = _budgetService.GetBudgetById(id);

        if (budget is null)
        {
            return NotFound();
        }

        var model = _getBudgetResponseBuilder.Build(new[] { budget });

        return Ok(model);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBudgetsResponse>), 200)]
    public async Task<ActionResult> GetBudgets(int pageSize = 20, int pageIndex = 0)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            return Unauthorized();
        }

        var budgets = _budgetService.GetUserBudgets(Guid.Parse(user.Id), pageIndex, pageSize);

        var model = _getBudgetResponseBuilder.Build(budgets);

        return Ok(model);
    }
}
