namespace ExpenseTracker.API.DTOs.User;

public class UpdateUserCommandRequest
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Password { get; set; }
}