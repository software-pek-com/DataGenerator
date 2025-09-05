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
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, 0.0);
  }

  /// <summary>
  /// Generates a value between 0.0 and smaller than the given maxValue.
  /// </summary>
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator, decimal maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(maxValue, nameof(maxValue));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "SmallerThan", 0.0, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue.
  /// </summary>
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator, decimal minValue, decimal maxValue)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "InRange", 0.0, minValue, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue with the given number of decimals.
  /// </summary>
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator, decimal minValue, decimal maxValue, int decimals)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "InRangeWithDecimals", 0.0, minValue, maxValue, decimals);
  }

  /// <summary>
  /// Returns a new decimal value of the given precision and scale.
  /// </summary>
  /// <remarks>
  /// Parameters are checked by the RandomExtensions.NextDecimal extension method used by <see cref="DecimalGenerator.OfPrecisionAndScale"/>.
  /// </remarks>
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator, uint precision, uint scale)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "OfPrecisionAndScale", 0.0, precision, scale);
  }

  /// <summary>
  /// Returns a new decimal value from one of the given values.
  /// </summary>
  public static void AsDecimal<T>(this PropertyDataGenerator<T, decimal> generator, params decimal[] values)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "OneOf", values);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue, or null with the given probability, for the given property value.
  /// </summary>
  public static void AsDecimalOrNull<T>(this PropertyDataGenerator<T, decimal?> generator, decimal minValue, decimal maxValue, double probabilityOfNull)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "InRange", probabilityOfNull, minValue, maxValue);
  }

  /// <summary>
  /// Generates a value between the given minValue and smaller than the maxValue, or null with the given probability, for the given property value, respecting the given decimal count.
  /// </summary>
  public static void AsDecimalOrNull<T>(this PropertyDataGenerator<T, decimal?> generator, decimal minValue, decimal maxValue, int decimals, double probabilityOfNull)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(minValue, maxValue, nameof(maxValue));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));

    generator.RegisterValueGenerator(_DecimalGenerator.Value, "InRangeWithDecimals", probabilityOfNull, minValue, maxValue, decimals);
  }
}