using System.ComponentModel.DataAnnotations;

namespace FamiliBudget.Api.Application.Models;

public class AddBudgetRequest
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Income { get; set; }

    public List<AddExpenseRequest>? Expenses { get; set; }
}
