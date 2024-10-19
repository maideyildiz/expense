namespace ExpenseTracker.API.DTOs;
public class UpdateUserCommandResult
{
    public required string Email { get; set; }
    public DateTime UpdatedDate { get; set; }

}