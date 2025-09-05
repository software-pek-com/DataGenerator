using DataGenerator.Core;

namespace DataGenerator.Extensions
{
  /// <summary>
  /// Represents extension methods for the Random class.
  /// </summary>
  public static class RandomExtensions
  {
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static readonly Random _Random = new();

    /// <summary>
    /// Returns true if a null value was generated given the probability of getting a null value.
    /// </summary>
    public static bool IsNextNull(this Random random, double nullProbability)
    {
      Guard.ArgumentNotNull(random, "random");
      Guard.ArgumentInRange(0.0, 1.0, nullProbability, "nullProbability");

      var randomValue = random.NextDouble();
      if (randomValue < nullProbability)
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Returns a decimal with a random value between 0.0 and 1.0.
    /// </summary>
    public static decimal NextDecimal(this Random random)
    {
      Guard.ArgumentNotNull(random, "random");

      return (decimal)random.NextDouble();
    }

    /// <summary>
    /// Returns a random non-negative decimal.
    /// </summary>
    /// <remarks>
    /// The most significant (29th) digit of decimal is incomplete and not supported by this method.
    /// Precision is limited to between 1 to 28 (not 29). To keep things simple 1st and last digits will never be zero.
    /// </remarks>
    public static decimal NextDecimal(this Random random, uint precision, uint scale)
    {
      Guard.ArgumentNotNull(random, "random");

      if (!(precision >= 1 && precision <= 28))
      {
        throw new ArgumentOutOfRangeException(nameof(precision), precision, "Precision must be between 1 and 28.");
      }

      if (scale > precision)
      {
        throw new ArgumentOutOfRangeException(nameof(scale), precision, "Scale must be between 0 and precision.");
      }

      decimal randomDecimal = 0m;
      var lastPosition = precision - 1;
      for (var pos = 0; pos <= lastPosition; ++pos)
      {
        decimal digit;
        if (pos == 0 || pos == lastPosition)
        {
          // First and last digits cannot be zero.
          do
          {
            digit = random.Next(0, 10);
          }
          while (digit == 0);
        }
        else
        {
          digit = random.Next(0, 10);
        }

        randomDecimal = randomDecimal * 10m + digit;
      }

      for (var s = 0; s < scale; ++s)
      {
        randomDecimal /= 10m;
      }

      return randomDecimal;
    }

    /// <summary>
    /// Returns a <see cref="Random"/> <see cref="string"/> of <paramref name="length"/>.
    /// </summary>
    public static string NextString(this Random random, int length)
    {
      return new string(Enumerable.Repeat(Chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Returns a <see cref="Random"/> <see cref="string"/> of <paramref name="length"/>.
    /// </summary>
    /// <remarks>
    /// The use of the <see cref="Random"/> class makes this method unsuitable for
    /// anything security related, such as creating passwords or tokens.
    /// Use the <see cref="System.Security.Cryptography.RNGCryptoServiceProvider"/>
    /// if you need a strong random number generator.
    /// </remarks>
    public static string GetRandomString(int length)
    {
      return _Random.NextString(length);
    }
  }
}