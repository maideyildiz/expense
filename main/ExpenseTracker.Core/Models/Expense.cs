namespace ExpenseTracker.Core.Models;

public class Expense : Base
{
    public string Note { get; set; } = string.Empty;
    public required Category Category { get; set; }
    public decimal Amount { get; set; }
}