namespace DataGenerator.Services
{
  /// <summary>
  /// This interface represents a data source for value of type <typeparam name="T"/>
  /// </summary>
  public interface IValueDataSource<T>
  {
    /// <summary>
    /// Gets all the data source's values.
    /// </summary>
    /// <returns>All the values.</returns>
    IEnumerable<T> GetAllValues();
  }
}
