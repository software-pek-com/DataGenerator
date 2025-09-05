using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of boolean values.
  /// </summary>
  public class BooleanGenerator : ValueGeneratorBase<bool>
  {
    /// <summary>
    /// Generates a random boolean value.
    /// </summary>
    public override bool New()
    {
      var i = RandomNumber.Next() % 2;
      return i == 0;
    }

    /// <summary>
    /// Generates a random boolean value with the given probability of true being returned.
    /// </summary>
    public bool NewBoolean(double probabilityOfTrue)
    {
      Guard.ArgumentBigger(0.0, probabilityOfTrue, nameof(probabilityOfTrue));
      if (probabilityOfTrue > 1.0)
      {
        throw new ArgumentOutOfRangeException(nameof(probabilityOfTrue), probabilityOfTrue, "Probability must be between 0 and 1.");
      }

      return RandomNumber.NextDouble() <= probabilityOfTrue;
    }

    /// <summary>
    /// Returns true.
    /// </summary>
    public bool AsTrue()
    {
      return true;
    }

    /// <summary>
    /// Returns false.
    /// </summary>
    public bool AsFalse()
    {
      return false;
    }
  }
}
