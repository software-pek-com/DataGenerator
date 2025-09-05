using DataGenerator.Core;
using DataGenerator.Services;
using Xunit;

namespace DataGenerator.Tests.Core
{
  public class DataSourceValueGeneratorTests
  {
    #region Helpers

    /// <summary>
    /// Mock implementation of <see cref="IValueDataSource{T}"/>
    /// </summary>
    private class ValueDataSourceMock<T> : IValueDataSource<T>
    {
      public IEnumerable<T> GetAllValues()
      {
        return GetAllValuesReturn!;
      }

      public IEnumerable<T>? GetAllValuesReturn { get; set; }
    }

    #endregion

    [Fact]
    public void Constructor_WithNullValueDataSource()
    {
      Assert.Throws<ArgumentNullException>(() => new DataSourceValueGenerator<string>(null!));
    }

    [Fact]
    public void Constructor_Ok()
    {
      var ValueDataSourceMock = new ValueDataSourceMock<string>();
      var target = new DataSourceValueGenerator<string>(ValueDataSourceMock);
    }

    [Fact]
    public void New_WithNoPossibleValue()
    {
      var ValueDataSourceMock = new ValueDataSourceMock<string>();
      ValueDataSourceMock.GetAllValuesReturn = new List<string>();

      var target = new DataSourceValueGenerator<string>(ValueDataSourceMock);

      Assert.Throws<InvalidOperationException>(() => target.New());
    }

    [Fact]
    public void New_WithOnePossibleValue()
    {
      var ValueDataSourceMock = new ValueDataSourceMock<string>();
      string possibleValue = "possible value";
      ValueDataSourceMock.GetAllValuesReturn = new[] { possibleValue };

      var target = new DataSourceValueGenerator<string>(ValueDataSourceMock);

      string result = target.New();

      Assert.Equal(possibleValue, result);
    }

    [Fact]
    public void New_WithSeveralPossibleValue()
    {
      var ValueDataSourceMock = new ValueDataSourceMock<string>();
      string possibleValue1 = "possible value 1";
      string possibleValue2 = "possible value 2";
      var possibleValues = new[] { possibleValue1, possibleValue2 };
      ValueDataSourceMock.GetAllValuesReturn = possibleValues;

      var target = new DataSourceValueGenerator<string>(ValueDataSourceMock);

      string result = target.New();

      Assert.Contains(result, possibleValues);
    }
  }
}
