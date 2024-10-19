namespace ExpenseTracker.API.DTOs;
public class GetUserListQueryResult
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }

}