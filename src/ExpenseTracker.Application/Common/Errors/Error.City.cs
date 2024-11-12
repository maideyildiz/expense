using ErrorOr;

namespace ExpenseTracker.Application.Common.Errors;

public static partial class Errors
{
    public static class City
    {
        public static Error NotFound => Error.NotFound("City.NotFound", "NotFound");
    }
}