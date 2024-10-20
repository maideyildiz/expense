namespace ExpenseTracker.API.DTOs.User;
public class InsertUserCommandResult
{
    public required string Email { get; set; }
    public DateTime CreatedDate { get; set; }

}