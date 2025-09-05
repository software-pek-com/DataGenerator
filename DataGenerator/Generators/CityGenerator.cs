using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// This generator generates cities.
  /// </summary>
  public class CityGenerator : ValueGeneratorBase<string>
  {
    /// <summary>
    /// Generates a new city.
    /// </summary>
    public override string New()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Generates a new city from a country.
    /// </summary>
    public string NewFromCountry(string country)
    {
      Guard.ArgumentNotNullOrWhitespace(country, nameof(country));

      var citiesByCountryText = ResourceHelper.Singleton.GetText(AssemblyInfo.Type, "CitiesByCountry");

      var countryAndCitiesLines = citiesByCountryText.Split('\n').ToList();

      var countriesAndCities = countryAndCitiesLines.Select(cc => cc.Split(':'));
      var citiesForCountryRaw = countriesAndCities
          .Where(cc => cc[0].Equals(country, StringComparison.InvariantCultureIgnoreCase))
          .Select(cc => cc[1])
          .SingleOrDefault();

      if (string.IsNullOrWhiteSpace(citiesForCountryRaw)) { return new StringGenerator().OfLength(10); }

      var citiesForCountry = citiesForCountryRaw.Split(',').Select(c => c.Trim()).ToArray();
      var cityIndex = RandomNumber.Next(0, citiesForCountry.Length);
      var result = citiesForCountry[cityIndex];
      return result;
    }
  }
}
