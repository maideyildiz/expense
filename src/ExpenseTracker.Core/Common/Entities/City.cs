using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;

namespace ExpenseTracker.Core.Common.Entities;

public class City : Entity<CityId>
{
    public string Name { get; }
    public Country Country { get; }
    public string ThreeLetterUAVTCode { get; }

    private City(CityId id, string name, Country country, string threeLetterUAVTCode)
        : base(id)
    {
        Name = name;
        Country = country;
        ThreeLetterUAVTCode = threeLetterUAVTCode;
    }

    public static City Create(string name, Country country, string threeLetterUAVTCode)
    {
        return new(CityId.CreateUnique(), name, country, threeLetterUAVTCode);
    }
}