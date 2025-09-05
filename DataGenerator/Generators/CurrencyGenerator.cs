using DataGenerator.Data;
using DataGenerator.Generators;

namespace OrganizationCsvGenerator
{
  /// <summary>
  /// Represents a generator of address data.
  /// </summary>
  public class CurrencyGenerator : JsonResourceFileGenerator<Currency>
  {
    public CurrencyGenerator() : base(typeof(Currency).Assembly, "Currencies.json")
    {
    }

    public List<Currency> GetAll()
    {
      return ValueDataSource.GetAllValues().ToList();
    }
  }
}
