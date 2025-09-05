using DataGenerator.Services;

namespace DataGenerator.Core
{
  /// <summary>
  /// This class represents a generator providing its values from a value data source.
  /// </summary>
  public class DataSourceValueGenerator<T> : ValueGeneratorBase<T>
  {
    private readonly IValueDataSource<T> valueDataSource;

    /// <summary>
    /// Internal for unit tests.
    /// </summary>
    internal protected IValueDataSource<T> ValueDataSource { get { return valueDataSource; } }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ValueDataSource">The data source for the values.</param>
    public DataSourceValueGenerator(IValueDataSource<T> valueDataSource)
    {
      Guard.ArgumentNotNull(valueDataSource, "valueDataSource");

      this.valueDataSource = valueDataSource;
    }

    /// <summary>
    /// Returns a new value from the list of possible values provided by the data source.
    /// </summary>
    public override T New()
    {
      var possibleValues = ValueDataSource.GetAllValues();

      int possibleValuesCount = possibleValues.Count();

      if (possibleValuesCount == 0)
      {
        throw new InvalidOperationException("There is no value to be provided");
      }

      var index = RandomNumber.Next(0, possibleValuesCount);

      return possibleValues.ElementAt(index);
    }
  }
}
