using ErrorOr;

namespace ExpenseTracker.Core.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation("Auth.InvalidCredentials", "The operation failed. Please check your information and try again.");
    }
}