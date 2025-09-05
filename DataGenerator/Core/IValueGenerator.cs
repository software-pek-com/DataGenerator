namespace DataGenerator.Core
{
  /// <summary>
  /// Represents a generator of values.
  /// </summary>
  public interface IValueGenerator
  {
    /// <summary>
    /// Returns a new value.
    /// </summary>
    object New();
  }

  /// <summary>
  /// Represents a generator of values of type <typeparamref name="T"/>.
  /// </summary>
  public interface IValueGenerator<out T> : IValueGenerator
  {
    /// <summary>
    /// Returns a new value of type <typeparamref name="T"/>.
    /// </summary>
    new T New();
  }
}
