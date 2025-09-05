using DataGeneratorApp;

namespace CompanyDataGenerator
{
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
}
