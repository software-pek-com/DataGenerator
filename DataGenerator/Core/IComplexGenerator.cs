namespace DataGenerator.Core
{
  /// <summary>
  /// Represents a complex generator marker interface.
  /// </summary>
  public interface IComplexGenerator { }

  /// <summary>
  /// Represents a delegate which called when an event is raised by a <see cref="IComplexGenerator{T}">complex generator</see>
  /// when an object requires finalization.
  /// </summary>
  public delegate void GeneratorFinalizer<in T>(T obj);

  /// <summary>
  /// Represents a generator of complex values. Complex values are ones which are in a
  /// relationship to other, potentially generated, values. The generation requires coordination
  /// to ensure a consistent set of values with respect to business requirements.
  /// </summary>
  public interface IComplexGenerator<out T> : IComplexGenerator where T : class, new()
  {
    /// <summary>
    /// Generates a new object.
    /// </summary>
    T Generate();

    /// <summary>
    /// Event which is raised just before the new object is generated i.e. before <see cref="Generate()"/> returns.
    /// </summary>
    event GeneratorFinalizer<T> Finalize;
  }
}
