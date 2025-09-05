using System.Text;

namespace CompanyDataGenerator
{
  internal class Program
  {
    private static Random random = new Random();

    static async Task Main(string[] args)
    {
      var rowCount = args.Length < 1 ? 10 : int.Parse(args[0]);
      var dataFileName = args.Length < 2
        ? $"CompanyData-{rowCount}-{Guid.NewGuid()}.csv"
        : args[1];

      var configuration = CsvWriterHelper.GetDefaultConfiguration();
      configuration.Delimiter = "|";

      using var writer = new StreamWriter(dataFileName, false, Encoding.UTF8);
      using var csvWriter = CsvWriterHelper.CreateWriter(writer, configuration);

      await CsvWriterHelper.WriteHeaders<CompanyData>(csvWriter);

      var generator = new CompanyDataGenerator();

      for (var i = 0; i < rowCount; ++i)
      {
        var company = generator.Generate();

        company.CountryCode = generator.MapToIsoCode(company.CountryCode!);

        GenerateFinancials(company);

        await CsvWriterHelper.WriteRecordsAsync(csvWriter, [company], "");
      }

      Console.WriteLine($"Output written to '{dataFileName}");

      await csvWriter.FlushAsync();
    }

    private static void GenerateFinancials(CompanyData company)
    {
      int financial05SalesVolume;
      int financial03SalesVolume;
      int financial01SalesVolume;

      if (random.Next(100) % 2 == 0) // Growing
      {
        financial05SalesVolume = random.Next(1000000, 80000000);
        financial03SalesVolume = random.Next(financial05SalesVolume, 90000000);
        financial01SalesVolume = random.Next(financial03SalesVolume, 100000000);
      }
      else // Shrinking
      {
        financial05SalesVolume = random.Next(1000000, 100000000);
        financial03SalesVolume = random.Next(1000000, financial05SalesVolume);
        financial01SalesVolume = random.Next(1000000, financial03SalesVolume);
      }

      var threeYearGrowth = (financial01SalesVolume - financial03SalesVolume) / (float)financial03SalesVolume;
      var fiveYearGrowth = (financial01SalesVolume - financial05SalesVolume) / (float)financial05SalesVolume;

      company.ThreeYearSalesGrowthPercantage = threeYearGrowth * 100;
      company.FiceYearSalesGrowthPercantage = fiveYearGrowth * 100;
    }
  }
}
