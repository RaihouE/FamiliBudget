using System.ComponentModel.DataAnnotations;

namespace FamiliBudget.Api.Application.Models;

public class AddExpenseRequest
{
    [Required]
    public string Type { get; set; } = null!;

    [Required]
    public decimal Value { get; set; }
}
