using DataGenerator.Core;
using DataGenerator.Core;
using System.Reflection;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator for a specific property.
  /// </summary>
  public class PropertyDataGenerator<T, TResult> where T : class, new()
  {
    /// <summary>
    /// Initializes this instance with a data generator.
    /// </summary>
    public PropertyDataGenerator(DataGenerator<T> parent, PropertyInfo property)
    {
      Guard.ArgumentNotNull(parent, nameof(parent));
      Guard.ArgumentNotNull(property, nameof(property));

      Parent = parent;
      Property = property;
    }

    /// <summary>
    /// Registers the given generator as a value source
    /// </summary>
    public void RegisterValueGenerator(IValueGenerator generator)
    {
      Guard.ArgumentNotNull(generator, nameof(generator));

      Parent.RegisterPropertyGenerator(Property, generator, 0.0);
    }

    /// <summary>
    /// Registers the given generator as a value source and a probability of getting a null value.
    /// </summary>
    public void RegisterValueGenerator(IValueGenerator generator, double nullProbability)
    {
      Guard.ArgumentNotNull(generator, nameof(generator));
      Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

      Parent.RegisterPropertyGenerator(Property, generator, nullProbability);
    }

    /// <summary>
    /// Registers the given generator as a value source, a method to call with parameters.
    /// </summary>
    public void RegisterValueGenerator(IValueGenerator generator, string methodToCall, params object[] args)
    {
      Guard.ArgumentNotNull(generator, nameof(generator));
      Guard.ArgumentNotNullOrWhitespace(methodToCall, nameof(methodToCall));

      RegisterValueGenerator(generator, methodToCall, 0.0, args);
    }

    /// <summary>
    /// Registers the given generator as a value source, a method to call with parameters and a probability of getting a null value.
    /// </summary>
    public void RegisterValueGenerator(IValueGenerator generator, string methodToCall, double nullProbability, params object[] args)
    {
      Guard.ArgumentNotNull(generator, nameof(generator));
      Guard.ArgumentNotNullOrWhitespace(methodToCall, nameof(methodToCall));
      Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

      Parent.RegisterPropertyGenerator(Property, generator, methodToCall, nullProbability, args);
    }

    public DataGenerator<T> Parent { get; }
    public PropertyInfo Property { get; }
  }
}
