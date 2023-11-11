using FamiliBudget.Api.Domain.Dictionaries;

namespace FamiliBudget.Api.Application;

public static class ExpenseTypeMapper
{
    public static ExpenseType Map(string type) => type switch
    {
        "food" => ExpenseType.Food,
        "housing" => ExpenseType.Housing,
        "debt" => ExpenseType.Debt,
        "health" => ExpenseType.Health,
        "fun" => ExpenseType.Fun,
        _ => ExpenseType.Other,
    };
}
