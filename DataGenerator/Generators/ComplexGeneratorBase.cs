using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a base class for <see cref="IComplexGenerator">complex generators</see>.
  /// </summary>
  public abstract class ComplexGeneratorBase<T> : IComplexGenerator<T>
    where T : class, new()
  {
    /// <summary>
    /// The random number generator instance.
    /// </summary>
    private static readonly Lazy<Random> _RandomNumber = new();

    /// <summary>
    /// Calls all registered finalizers with the new object.
    /// </summary>
    private void OnFinalize(T obj)
    {
      if (Finalize is not null)
      {
        Finalize.Invoke(obj);
      }
    }

    /// <summary>
    /// Called before finalize.
    /// </summary>
    protected virtual void OnBeforeFinalize(T obj) { }

    /// <summary>
    /// Called after finalize but before publish.
    /// </summary>
    protected virtual void OnAfterFinalize(T obj) { }

    /// <summary>
    /// Creates a new instance of <typeparamref name="T"/> using the 'new' operator.
    /// </summary>
    protected virtual T CreateNew()
    {
      return new T();
    }

    /// <summary>
    /// Generates an object of type <typeparamref name="T"/>.
    /// </summary>
    public T Generate()
    {
      var obj = CreateNew();

      OnBeforeFinalize(obj);

      OnFinalize(obj);

      OnAfterFinalize(obj);

      return obj;
    }

    /// <summary>
    /// Gets a random number generator.
    /// </summary>
    /// <remarks>
    /// This is a shortcut, helper property often required by implementations.
    /// </remarks>
    protected static Random RandomNumber { get { return _RandomNumber.Value; } }

    /// <summary>
    /// Event which is raised just before the new object is created (i.e. before <see cref="Generate()"/> returns).
    /// </summary>
    public event GeneratorFinalizer<T>? Finalize;
  }
}
