namespace ExpenseTracker.API.DTOs;
public class InsertUserCommandRequest
{
    public required string Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public required string Password { get; set; }

}