namespace FamiliBudget.Api.Domain.Entities;

public class Budget
{
    public Guid UserId { get; set; }
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Income { get; set; }
    public virtual List<Expense>? Expenses { get; set; }
}
