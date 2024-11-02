namespace ExpenseTracker.Core.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected Entity(TId id) => Id = id;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || obj.GetType() != this.GetType()) return false;

        return obj is Entity<TId> entity && EqualityComparer<TId>.Default.Equals(Id, entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (a is null) return b is null;
        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id is not null ? Id.GetHashCode() : 0;
    }
}
