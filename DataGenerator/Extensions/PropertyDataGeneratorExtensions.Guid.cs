using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a new Guid for the given property value.
  /// </summary>
  public static void AsGuid<T>(this PropertyDataGenerator<T, Guid> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_GuidGenerator.Value);
  }

  /// <summary>
  /// Generates a new Guid for the given property value.
  /// </summary>
  public static void AsGuidOrNull<T>(this PropertyDataGenerator<T, Guid?> generator, double nullProbability)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));
    generator.RegisterValueGenerator(_GuidGenerator.Value, nullProbability);
  }
}
