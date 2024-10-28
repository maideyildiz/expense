namespace ExpenseTracker.Core.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
where TId : notnull
{
    public TId Id { get; protected set; }
    protected Entity(TId id) => this.Id = id;

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && this.Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return this.Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !Equals(a, b);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}