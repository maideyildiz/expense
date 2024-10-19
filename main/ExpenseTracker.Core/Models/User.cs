namespace ExpenseTracker.Core.Models;
public class User : Base
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string Password { get; set; }
    public List<Expense>? Expenses { get; set; }
}
