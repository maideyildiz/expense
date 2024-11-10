using ExpenseTracker.Core.Common.Base;
namespace ExpenseTracker.Core.Entities;

public class City : Entity
{
    public City()
    {

    }
    public string Name { get; private set; }
    public Guid CountryId { get; private set; }
    public string ThreeLetterUAVTCode { get; private set; }

    private City(Guid id, string name, Guid countryId, string threeLetterUAVTCode)
    {
        Id = id;
        Name = name;
        CountryId = countryId;
        ThreeLetterUAVTCode = threeLetterUAVTCode;
    }

    public static City Create(string name, Guid countryId, string threeLetterUAVTCode)
    {
        return new(Guid.NewGuid(), name, countryId, threeLetterUAVTCode);
    }
}