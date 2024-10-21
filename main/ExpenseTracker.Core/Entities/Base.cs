namespace ExpenseTracker.Core.Entities;

public class Base
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public static (string columnNames, string parameterNames, string setClause) GetInsertAndUpdateColumns<T>() where T : Base
    {
        var properties = typeof(T).GetProperties();
        var columnNames = string.Join(", ", properties.Select(p => p.Name));
        var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));

        // Güncelleme için SET ifadesi oluşturma
        var setClause = string.Join(", ", properties
            .Where(p => p.Name != nameof(Base.Id)) // Id'yi hariç tut
            .Select(p => $"{p.Name} = @{p.Name}"));

        return (columnNames, parameterNames, setClause);
    }


}