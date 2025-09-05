using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates the given property value using the given lambda.
  /// </summary>
  public static void AsNew<T, TResult>(this PropertyDataGenerator<T, TResult> generator, Func<TResult> factoryMethod)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(factoryMethod, nameof(factoryMethod));

    var dynamicGenerator = new DynamicValueGenerator<TResult>(factoryMethod);
    generator.RegisterValueGenerator(dynamicGenerator);
  }
}
