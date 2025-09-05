using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator for integer values.
  /// </summary>
  public sealed class IntegerGenerator : ValueGeneratorBase<int>
  {
    private readonly int _Min;
    private readonly int _Max;

    /// <summary>
    /// Initializes min and max to default values (MinValue, MaxValue)
    /// </summary>
    public IntegerGenerator()
    {
      _Min = int.MinValue;
      _Max = int.MaxValue;
    }

    /// <summary>
    /// Initializes min and max to given values.
    /// Note 'max' is not included in the range.
    /// </summary>
    public IntegerGenerator(int min, int max)
    {
      if (min >= max)
      {
        throw new ArgumentException("'min' must be less than 'max'.");
      }

      Guard.ArgumentBigger(0, max, nameof(max));

      _Min = min;
      _Max = max;
    }

    /// <summary>
    /// Returns a new integer value in the allowed integer range i.e [int.MinValue, int.MaxValue).
    /// Note int.MaxValue is not included in the range.
    /// </summary>
    public override int New()
    {
      return InRange(_Min, _Max);
    }

    /// <summary>
    /// Returns a new integer value in the specified range i.e [0, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public int SmallerThan(int max)
    {
      Guard.ArgumentBigger(0, max, nameof(max));

      return InRange(0, max);
    }

    /// <summary>
    /// Returns a new integer value in the specified range i.e [min, max).
    /// Note 'max' is not included in the range.
    /// </summary>
    public int InRange(int min, int max)
    {
      if (min >= max)
      {
        throw new ArgumentException("'min' must be less than 'max'.");
      }

      Guard.ArgumentBigger(0, max, nameof(max));

      return RandomNumber.Next(min, max);
    }
  }
}
