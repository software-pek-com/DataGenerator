using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a delegate based <see cref="IValueGenerator"/> which uses
  /// a provided delegate to generate instances of <typeparamref name="T"/>.
  /// </summary>
  public class DynamicValueGenerator<T> : ValueGeneratorBase<T>
  {
    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal readonly Func<T> _FactoryMethod;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DynamicValueGenerator(Func<T> factoryMethod)
    {
      Guard.ArgumentNotNull(factoryMethod, nameof(factoryMethod));

      _FactoryMethod = factoryMethod;
    }

    /// <summary>
    /// Returns a new instance of type <typeparamref name="T"/>.
    /// </summary>
    public override T New()
    {
      return _FactoryMethod();
    }
  }
}
