using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.Common.Entities;

public class Country : Entity<Guid>
{
    private readonly List<Guid> _cityIds = new();
    public string Name { get; private set; }
    public string ThreeLetterUAVTCode { get; private set; }
    public IReadOnlyList<Guid> CityIds => this._cityIds.AsReadOnly();
    private Country(Guid id, string name, string threeLetterUAVTCode)
        : base(id)
    {
        Name = name;
        ThreeLetterUAVTCode = threeLetterUAVTCode;
    }

    public static Country Create(string name, string threeLetterUAVTCode)
    {
        return new(Guid.NewGuid(), name, threeLetterUAVTCode);
    }
}