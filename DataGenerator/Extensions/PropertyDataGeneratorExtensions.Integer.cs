using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a new integer value in the range int.MinValue and int.MaxValue.
  /// </summary>
  public static void AsInteger<T>(this PropertyDataGenerator<T, int> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_IntegerGenerator.Value, 0.0);
  }

  /// <summary>
  /// Generates a new integer value in the range [0, max).
  /// Note 'max' is not included in the range.
  /// </summary>
  public static void AsInteger<T>(this PropertyDataGenerator<T, int> generator, int maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(maxValue, nameof(maxValue));
    generator.RegisterValueGenerator(_IntegerGenerator.Value, "SmallerThan", 0.0, maxValue);
  }

  /// <summary>
  /// Generates a new integer value in the specified range i.e [min, max).
  /// Note 'max' is not included in the range.
  /// </summary>
  public static void AsInteger<T>(this PropertyDataGenerator<T, int> generator, int minValue, int maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    generator.RegisterValueGenerator(_IntegerGenerator.Value, "InRange", 0.0, minValue, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue, or null with the given probability, for the given property value.
  /// </summary>
  public static void AsIntegerOrNull<T>(this PropertyDataGenerator<T, int?> generator, int minValue, int maxValue, double probabilityOfNull)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));
    generator.RegisterValueGenerator(_IntegerGenerator.Value, "InRange", probabilityOfNull, minValue, maxValue);
  }

  /// <summary>
  /// Generates a new integer value in the specified range i.e [min, max).
  /// Note 'max' is not included in the range.
  /// </summary>
  public static void AsInteger<T>(this PropertyDataGenerator<T, int?> generator, int minValue, int maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    generator.RegisterValueGenerator(_IntegerGenerator.Value, "InRange", 0.0, minValue, maxValue);
  }
}
