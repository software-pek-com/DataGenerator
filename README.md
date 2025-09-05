# DataGenerator

A C# solution for generating sample or test data.  
This project is designed to help seed applications, services, or databases with consistent and configurable demo/test datasets.
Contains a full set of unit tests.

---

## Features
- Generate random but realistic sample data (names, dates, numbers, etc.)
- Configurable data sources and ranges
- Support for CSV output format
- Easy integration into other .NET applications

---

## Example

For a data class `CompanyData`

```
public class CompanyData
{
  public string? Id { get; set; }
  public string? CompanyName { get; set; }
  public int EmployeesCount { get; set; }
  public long SalesVolume { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? PostCode { get; set; }
  public string? CountryCode { get; set; }
  public int YearStarted { get; set; }
  public string? Phone { get; set; }
  public string? Website { get; set; }
  public float ThreeYearSalesGrowthPercantage { get; set; }
  public float FiceYearSalesGrowthPercantage { get; set; }
  public ListedOnExchange ListedOnExchange { get; set; }
}
```
Define a `CompanyDataGenerator` with a fluent-like data generation specification in the constructor

```
public CompanyDataGenerator()
{
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
```

Then simply generate the required number of instances of `CompanyData` and output to a CSV file e.g.

```
using var writer = new StreamWriter(dataFileName, false, Encoding.UTF8);
using var csvWriter = CsvWriterHelper.CreateWriter(writer, configuration);

await CsvWriterHelper.WriteHeaders<CompanyData>(csvWriter);

var generator = new CompanyDataGenerator();

for (var i = 0; i < rowCount; ++i)
{
  var company = generator.Generate();

  company.CountryCode = generator.MapToIsoCode(company.CountryCode!);

  // Add some random financial data.
  GenerateFinancials(company);

  await CsvWriterHelper.WriteRecordsAsync(csvWriter, [company], "");
}

Console.WriteLine($"Output written to '{dataFileName}");

await csvWriter.FlushAsync();
}
```

### Build
Clone the repository and build the solution. Unit tests are provided.
