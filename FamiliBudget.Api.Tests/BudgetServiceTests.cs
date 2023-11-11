using FamiliBudget.Api.Application;
using FamiliBudget.Api.Domain;
using FamiliBudget.Api.Domain.Dictionaries;
using FamiliBudget.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FamiliBudget.Api.Tests;

public class BudgetServiceTests
{
    [Fact]
    public void ServiceAddsEmptyBudget()
    {
        var mockSet = new Mock<DbSet<Budget>>();

        var mockContext = new Mock<BudgetDbContext>();
        mockContext.Setup(m => m.Budgets).Returns(mockSet.Object);

        var service = new BudgetService(mockContext.Object);
        service.AddBudget(Guid.NewGuid(), "Test budget", 1_000_000, new List<Expense>());

        mockSet.Verify(m => m.Add(It.IsAny<Budget>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void ServiceAddsBudgetWithExpenses()
    {
        var mockSetBudget = new Mock<DbSet<Budget>>();
        var mockSetExpense = new Mock<DbSet<Expense>>();
        var expenses = new List<Expense> { new Expense { BudgetId = 0, Type = ExpenseType.Food, Value = 1000 }, new Expense { BudgetId = 0, Type = ExpenseType.Fun, Value = 20 } };

        var mockContext = new Mock<BudgetDbContext>();
        mockContext.Setup(m => m.Budgets).Returns(mockSetBudget.Object);
        mockContext.Setup(m => m.Expenses).Returns(mockSetExpense.Object);

        var service = new BudgetService(mockContext.Object);
        service.AddBudget(Guid.NewGuid(), "Test budget", 1_000_000, expenses);

        mockSetBudget.Verify(m => m.Add(It.IsAny<Budget>()), Times.Once());
        mockSetExpense.Verify(m => m.AddRange(It.IsAny<IEnumerable<Expense>>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
    }

    [Fact]
    public void ServiceGetsBudgetById()
    {
        var data = new List<Budget>
        {
            new Budget{ Id = 0, Name = "Budget 0"},
            new Budget{ Id = 1, Name = "Budget 2"},
            new Budget{ Id = 2, Name = "Budget 3"},
        }.AsQueryable();

        var mockSetBudget = new Mock<DbSet<Budget>>();
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

        var mockContext = new Mock<BudgetDbContext>();
        mockContext.Setup(c => c.Budgets).Returns(mockSetBudget.Object);

        var service = new BudgetService(mockContext.Object);
        var budget = service.GetBudgetById(1);

        Assert.NotNull(budget);
        Assert.Equal(1, budget.Id);
        Assert.Equal("Budget 2", budget.Name);
    }

    [Fact]
    public void ServiceGetsBudgetsForUser()
    {
        var data = new List<Budget>
        {
            new Budget{ Id = 0, Name = "Budget 0", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 1, Name = "Budget 1", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 2, Name = "Budget 2", UserId = Guid.Parse("eb59dfec-80b9-11ee-b962-0242ac120002")},
        }.AsQueryable();

        var mockSetBudget = new Mock<DbSet<Budget>>();
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

        var mockContext = new Mock<BudgetDbContext>();
        mockContext.Setup(c => c.Budgets).Returns(mockSetBudget.Object);

        var service = new BudgetService(mockContext.Object);
        var budgets = service.GetUserBudgets(Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002"), 0, 20);

        Assert.NotNull(budgets);
        Assert.Equal(2, budgets.Count);
        Assert.All(budgets, budget => Assert.Equal(Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002"), budget.UserId));
    }

    [Fact]
    public void ServiceGetsPagedBudgetsForUser()
    {
        var data = new List<Budget>
        {
            new Budget{ Id = 0, Name = "Budget 0", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 1, Name = "Budget 1", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 2, Name = "Budget 2", UserId = Guid.Parse("eb59dfec-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 3, Name = "Budget 3", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 4, Name = "Budget 4", UserId = Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002")},
            new Budget{ Id = 5, Name = "Budget 5", UserId = Guid.Parse("eb59dfec-80b9-11ee-b962-0242ac120002")},
        }.AsQueryable();

        var mockSetBudget = new Mock<DbSet<Budget>>();
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSetBudget.As<IQueryable<Budget>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

        var mockContext = new Mock<BudgetDbContext>();
        mockContext.Setup(c => c.Budgets).Returns(mockSetBudget.Object);

        var service = new BudgetService(mockContext.Object);
        var budgets = service.GetUserBudgets(Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002"), 1, 2);

        Assert.NotNull(budgets);
        Assert.Equal(2, budgets.Count);
        Assert.All(budgets, budget => Assert.Equal(Guid.Parse("d3a2d822-80b9-11ee-b962-0242ac120002"), budget.UserId));
        Assert.Equal(3, budgets[0].Id);
        Assert.Equal("Budget 3", budgets[0].Name);
        Assert.Equal(4, budgets[1].Id);
        Assert.Equal("Budget 4", budgets[1].Name);
    }
}