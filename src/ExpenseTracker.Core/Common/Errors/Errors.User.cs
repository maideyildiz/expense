using ErrorOr;

namespace ExpenseTracker.Core.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error UserMustHaveFirstAndLastName => Error.Conflict("User.UserMustHaveFirstAndLastName", "User must have first and last name.");
        public static Error UserMustHaveSubscription => Error.Conflict("User.UserMustHaveSubscription", "User must have subscription.");
        public static Error DublicateEmail => Error.Conflict("User.DublicateEmail", "The operation failed. Please check your information and try again.");
        public static Error UserNotFound => Error.NotFound("User.UserNotFound", "The operation failed. Please check your information and try again.");
    }
}