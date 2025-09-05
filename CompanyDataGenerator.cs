using CrypticWizard.RandomWordGenerator;
using DataGenerator.Core;
using DataGenerator.Extensions;
using DataGenerator.Generators;
using DataGenerator.Services;
using System.Diagnostics.Contracts;

namespace CompanyDataGenerator
{
  internal class CompanyDataGenerator : DataGenerator<CompanyData>
  {
    private readonly StreetNameGenerator streetNameGenerator = new();
    private readonly WordGenerator wordGenerator = new();
    private readonly List<WordGenerator.PartOfSpeech> namePattern = [WordGenerator.PartOfSpeech.adj, WordGenerator.PartOfSpeech.noun];

    private readonly Dictionary<string, string> countryIsoCodeMap;

    public CompanyDataGenerator()
    {
      countryIsoCodeMap = LoadMapFromResources();

      For(a => a.Id).AsNew(() => $"0{RandomNumber.Next(1000, 1000000)}{StringExtensions.GenerateRandom(2)}");
      For(a => a.CompanyName).AsNew(GetName);
      For(a => a.Address).AsNew(GetAddress1);
      For(a => a.PostCode).AsNew(() => RandomNumber.Next(1000, 10000).ToString());
      For(a => a.CountryCode).AsCountry();
      For(a => a.City).AsCity(x => x.CountryCode);
      For(a => a.Website).As(x => $"www.{GetUrlNameFrom(x.CompanyName!)}.com");
      For(a => a.YearStarted).AsNew(() => RandomNumber.Next(1970, 2024));
      For(a => a.EmployeesCount).AsNew(() => RandomNumber.Next(1, 2000));
      For(a => a.SalesVolume).AsNew(() => RandomNumber.Next(1000000, 100000000));
      For(a => a.ListedOnExchange).AsEnum();
      For(a => a.Phone).AsPhoneNumber();
    }

    /// <summary>
    /// Returns the IsoCode corresponding to <paramref name="countryName"/>.
    /// </summary>
    public string MapToIsoCode(string countryName)
    {
      Guard.ArgumentNotWhitespace(countryName, nameof(countryName));

      return countryIsoCodeMap[countryName!];
    }

    #region Internals

    private string GetName()
    {
      var threeRandomLetters = StringExtensions.GenerateRandom(3);
      var randomName = wordGenerator.GetPatterns(namePattern, ' ', 1);
      return $"{string.Join(' ', randomName)}".FirstToUpper();
    }

    private string GetAddress1()
    {
      var number = RandomNumber.Next(1, 100);
      var name = streetNameGenerator.New();

      return $"{number} {name}";
    }

    private string GetUrlNameFrom(string name)
    {
      return name.Replace(" ", "").ToLower();
    }

    private static Dictionary<string, string> LoadMapFromResources()
    {
      var countryIsoCodesProvider = new StringResourceFileDataSource("CountryIsoCodeMap.txt");
      var countryIsoCodes = countryIsoCodesProvider.GetAllValues();

      var map = new Dictionary<string, string>();

      foreach (var line in countryIsoCodes)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          continue;
        }
        var parts = line.Split([','], 2);
        if (parts.Length != 2)
        {
          throw new FormatException($"Wrong format of country IsoCode resource '{line}'.");
        }

        var key = parts[0].Trim();
        var value = parts[1].Trim();
        map[key] = value;
      }

      return map;
    }

    #endregion
  }
}
