namespace ExpenseTracker.API.DTOs;
public class InsertUserCommandResult
{
    public required string Email { get; set; }
    public DateTime CreatedDate { get; set; }

}