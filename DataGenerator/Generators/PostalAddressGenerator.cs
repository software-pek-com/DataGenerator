using DataGenerator.Core;
using System.Dynamic;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of postal addresses.
  /// </summary>
  /// <remarks>
  /// Not sealed for unit tests only.
  /// </remarks>
  public class PostalAddressGenerator : FormattedStringGenerator
  {
    private readonly IDictionary<string, IList<string>> _CitiesByCountry = new Dictionary<string, IList<string>>();
    private readonly CountryWithCitiesGenerator _CountryNameGenerator;

    /// <summary>
    /// Initializes this instance with a list of cities by country.
    /// </summary>
    public PostalAddressGenerator()
      : base("{0} {1}, {2}, {3} {4}")
    // 0 - house number
    // 1 = street name (e.g. Oxford Road)
    // 2 = city
    // 3 = postcode (5 digits as per EU standard)
    // 4 = country
    {
      var citiesByCountryText = ResourceHelper.Singleton.GetText(AssemblyInfo.Type, "CitiesByCountry");
      var citiesByCountryLines = citiesByCountryText.Split('\n').ToList();

      foreach (var countryLine in citiesByCountryLines)
      {
        var countryLineParts = countryLine.Split(':');
        var country = countryLineParts[0];
        var countryCitiesRaw = countryLineParts[1].Split(',').ToList();
        var countryCities = new List<string>(countryCitiesRaw.Count);
        foreach (var city in countryCitiesRaw)
        {
          countryCities.Add(city.Trim());
        }

        _CitiesByCountry.Add(country, countryCities);
      }

      var integerGenerator = new IntegerGenerator();
      _CountryNameGenerator = new CountryWithCitiesGenerator(_CitiesByCountry);

      RegisterValues(0, new HouseNumberGenerator(integerGenerator));
      RegisterValues(1, new StreetNameGenerator());
      RegisterValues(2, new List<string> { "X" } /* dummy city */);
      RegisterValues(3, new PostcodeGenerator(integerGenerator));
      RegisterValues(4, _CountryNameGenerator);
    }

    /// <summary>
    /// Throws <see cref="InvalidOperationException"/>.
    /// </summary>
    public override string New()
    {
      var country = (string)_CountryNameGenerator.New();
      dynamic addressObj = (ExpandoObject)NewAddress(country);
      return string.Format(Descriptor.Pattern,
        addressObj.HouseNumber,
        addressObj.StreetName,
        addressObj.City,
        addressObj.Postcode,
        addressObj.Country);
    }

    /// <summary>
    /// Returns a randomly generated address in the given country.
    /// </summary>
    public object NewAddress(string country)
    {
      var newAddress = new ExpandoObject() as IDictionary<string, object>;

      newAddress.Add("HouseNumber", ChooseValue(0));
      newAddress.Add("StreetName", ChooseValue(1));
      newAddress.Add("Postcode", ChooseValue(3));
      newAddress.Add("Country", country);

      var countryCities = _CitiesByCountry[country];
      // The max value used in 'Random.Next' is an exclusive bound so the count of items is sufficient.
      var randomCityId = RandomNumber.Next(0, countryCities.Count);
      var city = countryCities[randomCityId];

      newAddress.Add("City", city);

      return newAddress;
    }

    #region Internal Generators

    private class HouseNumberGenerator : IValueGenerator
    {
      private readonly IntegerGenerator _IntGenerator;

      /// <summary>
      /// Constructor.
      /// </summary>
      public HouseNumberGenerator(IntegerGenerator integerGenerator)
      {
        _IntGenerator = integerGenerator;
      }

      /// <summary>
      /// Generates a new house number i.e. an integer between 1 and 2000.
      /// </summary>
      public object New()
      {
        return _IntGenerator.InRange(1, 2000);
      }
    }

    private class PostcodeGenerator : IValueGenerator
    {
      private readonly IntegerGenerator _IntGenerator;

      /// <summary>
      /// Constructor.
      /// </summary>
      public PostcodeGenerator(IntegerGenerator integerGenerator)
      {
        _IntGenerator = integerGenerator;
      }

      /// <summary>
      /// Generates a new 5 digit postcode.
      /// </summary>
      public object New()
      {
        return _IntGenerator.InRange(10000, 99999);
      }
    }

    private class CountryWithCitiesGenerator : IValueGenerator
    {
      private readonly IDictionary<string, IList<string>> _CitiesByCountry;

      /// <summary>
      /// Constructor.
      /// </summary>
      public CountryWithCitiesGenerator(IDictionary<string, IList<string>> countryCityMap)
      {
        _CitiesByCountry = countryCityMap;
      }

      /// <summary>
      /// Generates a new 5 digit postcode.
      /// </summary>
      public object New()
      {
        var countriesList = _CitiesByCountry.Keys.ToList();
        var countryId = RandomNumber.Next(0, countriesList.Count);
        return countriesList[countryId];
      }
    }

    #endregion
  }
}
