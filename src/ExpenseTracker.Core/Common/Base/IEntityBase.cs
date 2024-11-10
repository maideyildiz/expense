namespace ExpenseTracker.Core.Common.Base;
public interface IEntityBase<TId>
{
    TId Id { get; }
}