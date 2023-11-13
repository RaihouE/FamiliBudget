using System.Text.Json.Serialization;

namespace FamiliBudget.App.Infrastructure;

public class BudgetResponse
{
	[JsonPropertyName("budgets")]
	public List<Budget> Budgets { get; set; } = new List<Budget>();

	public class Expense
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("value")]
		public decimal Value { get; set; }

		[JsonPropertyName("type")]
		public string? Type { get; set; }
	}
	public class Budget
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("income")]
		public decimal Income { get; set; }

		[JsonPropertyName("expenses")]
		public List<Expense> Expenses { get; set; } = new List<Expense>();
	}
}
