namespace ExpenseTracker.Core.Models;

public class Expense : Base
{
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
}