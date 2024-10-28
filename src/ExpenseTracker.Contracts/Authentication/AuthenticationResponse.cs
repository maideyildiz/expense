namespace ExpenseTracker.Contracts.Authentication;
public record AuthenticationResponse
(
    string Token,
    string Email,
    string Name,
    string Surname
);