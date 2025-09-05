using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates true for the given property value.
  /// </summary>
  public static void AsTrue<T>(this PropertyDataGenerator<T, bool> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_BooleanGenerator.Value, "AsTrue");
  }

  /// <summary>
  /// Generates false for the given property value.
  /// </summary>
  public static void AsFalse<T>(this PropertyDataGenerator<T, bool> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_BooleanGenerator.Value, "AsFalse");
  }

  /// <summary>
  /// Generates true with the given probability for the given property value.
  /// </summary>
  public static void AsBool<T>(this PropertyDataGenerator<T, bool> generator, double probabilityOfTrue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfTrue, nameof(probabilityOfTrue));
    generator.RegisterValueGenerator(_BooleanGenerator.Value, "NewBoolean", 0.0, probabilityOfTrue);
  }

  /// <summary>
  /// Generates true or false with equal probability, or null with the given probability, for the given property value.
  /// </summary>
  public static void AsBoolOrNull<T>(this PropertyDataGenerator<T, bool?> generator, double probabilityOfNull)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));
    generator.RegisterValueGenerator(_BooleanGenerator.Value, probabilityOfNull);
  }
}
