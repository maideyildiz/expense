namespace ExpenseTracker.API.DTOs.User;
public class InsertUserCommandRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Password { get; set; }

}