using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates the given property using the given generator type.
  /// </summary>
  public static void AsGenerator<T, TResult>(this PropertyDataGenerator<T, TResult> propertyGen, IComplexGenerator generator)
      where T : class, new()
      where TResult : class, new()
  {
    Guard.ArgumentNotNull(propertyGen, nameof(propertyGen));
    Guard.ArgumentNotNull(generator, nameof(generator));

    propertyGen.Parent.RegisterPropertyGenerator(propertyGen.Property, generator);
  }

  /// <summary>
  /// Generates true for the given property value.
  /// </summary>
  public static void AsGenerator<T, TResult>(this PropertyDataGenerator<T, TResult> propertyGen, IComplexGenerator<TResult> generator, Action<TResult> finalizeAction)
      where T : class, new()
      where TResult : class, new()
  {
    Guard.ArgumentNotNull(propertyGen, nameof(propertyGen));
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(finalizeAction, nameof(finalizeAction));

    propertyGen.Parent.RegisterPropertyGenerator(propertyGen.Property, generator, finalizeAction);
  }
}
