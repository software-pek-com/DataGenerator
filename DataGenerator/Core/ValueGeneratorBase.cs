namespace DataGenerator.Core
{
  /// <summary>
  /// Represents a base class for generators of values of type <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T"/>
  public abstract class ValueGeneratorBase<T> : IValueGenerator<T>
  {
    private static readonly Lazy<Random> _RandomNumber = new Lazy<Random>(true);

    /// <summary>
    /// Returns a new value of type <typeparamref name="T"/>.
    /// </summary>
    public abstract T New();

    /// <summary>
    /// Returns a new value.
    /// </summary>
    object IValueGenerator.New()
    {
      return New()!;
    }

    /// <summary>
    /// Gets the random number generator.
    /// </summary>
    protected static Random RandomNumber { get { return _RandomNumber.Value; } }
  }
}
