namespace ExpenseTracker.API.DTOs.User;
public class UpdateUserCommandResult
{
    public required string Email { get; set; }
    public DateTime UpdatedDate { get; set; }

}