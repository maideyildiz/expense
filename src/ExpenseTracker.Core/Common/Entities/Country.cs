using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;

namespace ExpenseTracker.Core.Common.Entities;

public class Country : Entity<CountryId>
{
    private readonly List<CityId> _cityIds = new();
    public string Name { get; }
    public string ThreeLetterUAVTCode { get; }
    public IReadOnlyList<CityId> CityIds => this._cityIds.AsReadOnly();
    private Country(CountryId id, string name, string threeLetterUAVTCode)
        : base(id)
    {
        Name = name;
        ThreeLetterUAVTCode = threeLetterUAVTCode;
    }

    public static Country Create(string name, string threeLetterUAVTCode)
    {
        return new(CountryId.CreateUnique(), name, threeLetterUAVTCode);
    }
}