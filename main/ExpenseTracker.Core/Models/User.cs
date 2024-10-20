namespace ExpenseTracker.Core.Models;
public class User : Base
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string Password { get; set; }
    public List<Expense>? Expenses { get; set; }
    // public string? JwtToken { get => _jwtToken; }

    // private string? _jwtToken { get; set; }

    // public void SetJwtToken(string token)
    // {
    //     _jwtToken = token;
    // }
}
