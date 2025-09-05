using DataGenerator.Core;
using DataGenerator.Extensions;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator for decimal values.
  /// </summary>
  /// <remarks>
  /// Decimals can be:
  /// <para>in a range of values,</para>
  /// <para>bigger than a min value,</para>
  /// <para>smaller than a max value,</para>
  /// <para>of a certain precision,</para>
  /// <para>use a certain number of significant digits,</para>
  /// <para>null to ensure nullable decimal values are represented.</para>
  /// </remarks>
  public sealed class DecimalGenerator : ValueGeneratorBase<decimal>
  {
    /// <summary>
    /// Returns a new decimal value in the range [0.0, 1.0).
    /// Note 1.0 is not included in the range.
    /// </summary>
    public override decimal New()
    {
      return RandomNumber.NextDecimal();
    }

    /// <summary>
    /// Returns a new decimal value in the specified range i.e [0, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public decimal SmallerThan(decimal max)
    {
      Guard.ArgumentBigger(0m, max, nameof(max));

      return RandomNumber.NextDecimal() * max;
    }

    /// <summary>
    /// Returns a new decimal value in the specified range i.e [min, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public decimal InRange(decimal min, decimal max)
    {
      if (min >= max)
      {
        throw new ArgumentException("'min' must be less than 'max'.");
      }

      Guard.ArgumentBigger(0m, max, nameof(max));

      return min + RandomNumber.NextDecimal() * (max - min);
    }

    /// <summary>
    /// Returns a new decimal value in the specified range i.e [min, max) with the given number of decimals.
    /// Note 'max' is not included in the range.
    /// </summary>
    public decimal InRangeWithDecimals(decimal min, decimal max, int decimals)
    {
      if (min >= max)
      {
        throw new ArgumentException("'min' must be less than 'max'.");
      }

      Guard.ArgumentBigger(0m, max, nameof(max));
      Guard.ArgumentBigger(-1, decimals, nameof(decimals));

      var value = min + RandomNumber.NextDecimal() * (max - min);

      return Math.Round(value, decimals);
    }

    /// <summary>
    /// Returns a new decimal value from the given set.
    /// </summary>
    public decimal OneOf(params decimal[] values)
    {
      if (values.Length == 0)
      {
        throw new ArgumentException("There must be more than zero given values.");
      }

      int index = RandomNumber.Next(values.Length);
      return values[index];
    }

    /// <summary>
    /// Returns a new decimal value of the given precision and scale.
    /// </summary>
    /// <remarks>
    /// Parameters are checked by the RandomExtensions.NextDecimal extension method.
    /// </remarks>
    public decimal OfPrecisionAndScale(uint precision, uint scale)
    {
      return RandomNumber.NextDecimal(precision, scale);
    }
  }
}
