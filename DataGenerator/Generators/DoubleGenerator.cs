using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator for double values.
  /// </summary>
  /// <remarks>
  /// Doubles can be:
  /// <para>in a range of values,</para>
  /// <para>bigger than a min value,</para>
  /// <para>smaller than a max value,</para>
  /// <para>including or excluding one of the above bounds,</para>
  /// <para>negative,</para>
  /// <para>null to ensure nullable double values are represented</para>
  /// </remarks>
  public sealed class DoubleGenerator : ValueGeneratorBase<double>
  {
    /// <summary>
    /// Returns a new double value in the range [0.0, 1.0).
    /// Note 1.0 is not included in the range.
    /// </summary>
    public override double New()
    {
      return RandomNumber.NextDouble();
    }

    /// <summary>
    /// Returns a new double value in the specified range i.e [0.0, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public double SmallerThan(double max)
    {
      Guard.ArgumentBigger(0.0, max, nameof(max));

      return RandomNumber.NextDouble() * max;
    }

    /// <summary>
    /// Returns a new double value in the specified range i.e [min, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public double InRange(double min, double max)
    {
      if (min >= max)
      {
        throw new ArgumentException("'min' must be less than 'max'.");
      }

      Guard.ArgumentBigger(0.0, max, nameof(max));

      return min + RandomNumber.NextDouble() * max;
    }
  }
}
