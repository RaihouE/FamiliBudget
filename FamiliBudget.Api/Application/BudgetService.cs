using FamiliBudget.Api.Domain;
using FamiliBudget.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamiliBudget.Api.Application;

public class BudgetService
{
    private readonly BudgetDbContext _dbContext;

    public BudgetService(BudgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddBudget(Guid userId, string name, decimal income, IReadOnlyCollection<Expense> expenses)
    {
        var newBudget = new Budget { Name = name, Income = income, Expenses = new List<Expense>(), UserId = userId };

        _dbContext.Budgets.Add(newBudget);

        _dbContext.SaveChanges();

        if (expenses.Any())
        {
            var expensesToAdd = expenses.Select(e => new Expense { BudgetId = newBudget.Id, Type = e.Type, Value = e.Value });

            _dbContext.Expenses.AddRange(expensesToAdd);

            _dbContext.SaveChanges();
        }

        return newBudget.Id;
    }

    public Budget? GetBudgetById(int id)
    {
        var budget = _dbContext.Budgets
            .AsNoTracking()
            .Include(b => b.Expenses)
            .FirstOrDefault(e => e.Id == id);

        return budget;
    }

    public List<Budget> GetUserBudgets(Guid userId, int pageIndex, int pageSize)
    {
        var budgets = _dbContext.Budgets
            .AsNoTracking()
            .Include(b => b.Expenses)
            .Where(b => b.UserId == userId)
            .Skip(pageIndex * pageSize)
            .Take(pageSize);

        return budgets.ToList();
    }
}
