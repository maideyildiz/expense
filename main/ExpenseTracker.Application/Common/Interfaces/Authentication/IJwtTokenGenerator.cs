namespace ExpenseTracker.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string name, string surname);
}