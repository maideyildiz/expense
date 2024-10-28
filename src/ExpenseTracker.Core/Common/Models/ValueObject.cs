namespace ExpenseTracker.Core.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }
        var other = (ValueObject)obj;
        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject a, ValueObject b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !Equals(a, b);
    }

    public override int GetHashCode()
    {
        return this.GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other)
    {
        return this.Equals((object?)other);
    }
}