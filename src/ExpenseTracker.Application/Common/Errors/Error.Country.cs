using ErrorOr;

namespace ExpenseTracker.Application.Common.Errors;

public static partial class Errors
{
    public static class Country
    {
        public static Error NotFound => Error.NotFound("Country.NotFound", "NotFound");
    }
}