using ErrorOr;

namespace ExpenseTracker.Core.Common.Errors;

public static class Errors
{
    public static class User
    {
        public static Error DublicateEmail => Error.Conflict("User.DublicateEmail", "Email already exists.");
        public static Error UserNotFound => Error.NotFound("User.UserNotFound", "User not found or password is wrong.");
        public static Error CredentialsError => Error.Validation("User.CredentialsError", "Invalid email or password.");
    }
}