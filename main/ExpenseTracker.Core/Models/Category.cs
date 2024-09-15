namespace ExpenseTracker.Core.Models;

public class Category : Base
{
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public IList<Expense> Expenses { get; set; }= new List<Expense>();
}