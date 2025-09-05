using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class CityGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      new CityGenerator();
    }

    #region New

    [Fact]
    public void New()
    {
      var target = new CityGenerator();

      Assert.Throws<NotSupportedException>(() => target.New());
    }

    #endregion

    #region NewFromCountry

    [Fact]
    public void NewFromCountry_WithNullCountryCode()
    {
      var target = new CityGenerator();

      Assert.Throws<ArgumentNullException>(() => target.NewFromCountry(null!));
    }

    [Fact]
    public void NewFromCountry_WithEmptyCountryCode()
    {
      var target = new CityGenerator();

      Assert.Throws<ArgumentException>(() => target.NewFromCountry(""));
    }

    [Fact]
    public void NewFromCountry_WithWhitespaceCountryCode()
    {
      var target = new CityGenerator();

      Assert.Throws<ArgumentException>(() => target.NewFromCountry("     "));
    }

    [Fact]
    public void NewFromCountry_UnknownCity()
    {
      var target = new CityGenerator();

      var result = target.NewFromCountry("unknown city");

      Assert.NotNull(result);
      Assert.Equal(10, result.Length);
    }

    [Fact]
    public void NewFromCountry_Ok()
    {
      var target = new CityGenerator();

      var result = target.NewFromCountry("belgium");

      Assert.NotNull(result);
    }

    #endregion 
  }
}
