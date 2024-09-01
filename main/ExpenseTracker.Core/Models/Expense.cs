namespace ExpenseTracker.Core.Models;

public class Expense : Base
{
    public string Note { get; set; }
    public Category Category { get; set; }
    public decimal Amount { get; set; }
}