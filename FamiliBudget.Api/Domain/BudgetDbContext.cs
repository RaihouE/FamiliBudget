using FamiliBudget.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamiliBudget.Api.Domain;

public class BudgetDbContext : DbContext
{
    public virtual DbSet<Budget> Budgets { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }

    public BudgetDbContext() { }

    public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
