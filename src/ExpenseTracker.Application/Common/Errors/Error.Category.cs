using ErrorOr;

namespace ExpenseTracker.Application.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NotFound => Error.NotFound("City.NotFound", "NotFound");
    }
}