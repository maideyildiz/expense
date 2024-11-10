using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Core.Entities;

public class Country : Entity
{
    public string Name { get; private set; }
    public string ThreeLetterUAVTCode { get; private set; }
    public IReadOnlyList<City> Cities { get; private set; }
    private Country(Guid id, string name, string threeLetterUAVTCode)
    {
        Id = id;
        Name = name;
        ThreeLetterUAVTCode = threeLetterUAVTCode;
    }

    public static Country Create(string name, string threeLetterUAVTCode)
    {
        return new(Guid.NewGuid(), name, threeLetterUAVTCode);
    }
}