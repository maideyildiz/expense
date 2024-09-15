namespace ExpenseTracker.Core.Models;

public class Expense : Base
{
    public string Note { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}