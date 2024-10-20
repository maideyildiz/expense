namespace ExpenseTracker.API.DTOs.User;
public class GetUserListQueryResult
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }

}