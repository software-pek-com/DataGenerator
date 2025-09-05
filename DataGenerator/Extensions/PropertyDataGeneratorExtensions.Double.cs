using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a new double value in the range [0.0, 1.0).
  /// </summary>
  public static void AsDouble<T>(this PropertyDataGenerator<T, double> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    generator.RegisterValueGenerator(_DoubleGenerator.Value, 0.0);
  }

  /// <summary>
  /// Generates a value between 0.0 and smaller than the given maxValue.
  /// </summary>
  public static void AsDouble<T>(this PropertyDataGenerator<T, double> generator, double maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(maxValue, nameof(maxValue));

    generator.RegisterValueGenerator(_DoubleGenerator.Value, "SmallerThan", 0.0, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue.
  /// </summary>
  public static void AsDouble<T>(this PropertyDataGenerator<T, double> generator, double minValue, double maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));

    generator.RegisterValueGenerator(_DoubleGenerator.Value, "InRange", 0.0, minValue, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue, or null with the given probability, for the given property value.
  /// </summary>
  public static void AsDoubleOrNull<T>(this PropertyDataGenerator<T, double?> generator, double minValue, double maxValue, double probabilityOfNull)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));

    generator.RegisterValueGenerator(_DoubleGenerator.Value, "InRange", probabilityOfNull, minValue, maxValue);
  }
}

