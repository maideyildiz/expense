using ErrorOr;

namespace ExpenseTracker.Application.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error InvalidUserId => Error.Validation("User.InvalidUserId", "The operation failed. Please check your information and try again.");
        public static Error UserMustHaveFirstAndLastName => Error.Conflict("User.UserMustHaveFirstAndLastName", "User must have first and last name.");
        public static Error DublicateEmail => Error.Conflict("User.DublicateEmail", "The operation failed. Please check your information and try again.");
        public static Error UserNotFound => Error.NotFound("User.UserNotFound", "The operation failed. Please check your information and try again.");
    }
}