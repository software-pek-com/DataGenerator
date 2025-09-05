using DataGenerator.Core;
using DataGenerator.Extensions;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of strings of different values including null and empty.
  /// </summary>
  public sealed class StringGenerator : IValueGenerator
  {
    private readonly Lazy<Random> _RandomNumber = new Lazy<Random>(true);
    private readonly int _MinLength;
    private readonly int _MaxLength;

    /// <summary>
    /// Initializes an instance that will generate random strings with a length that is randomly in the range [0, 32].
    /// </summary>
    public StringGenerator()
    {
      _MinLength = 0;
      _MaxLength = 32;
    }

    /// <summary>
    /// Initializes an instance that will generate random strings with a length that is randomly in the range
    /// [<paramref name="minLength"/>, <paramref name="maxLength"/>].
    /// </summary>
    /// <param name="minLength">The min length for the random strings; must be &gt;= 0</param>
    /// <param name="maxLength">The max length for the random strings (note: this value in included in the range);
    /// must be &gt;= <paramref name="minLength"/></param>
    public StringGenerator(int minLength, int maxLength)
    {
      Guard.ArgumentBigger(-1, minLength, nameof(minLength));
      Guard.ArgumentBigger(minLength - 1, maxLength, nameof(maxLength));

      _MinLength = minLength;
      _MaxLength = maxLength;
    }

    /// <summary>
    /// Returns a random string of random length.
    /// </summary>
    public object New()
    {
      int length = _RandomNumber.Value.Next(_MinLength, _MaxLength + 1);
      string result = StringExtensions.GenerateRandom(length);
      return result;
    }

    /// <summary>
    /// Returns a random string of the given length.
    /// </summary>
    public string OfLength(int length)
    {
      Guard.ArgumentBigger(0, length, nameof(length));

      string result = StringExtensions.GenerateRandom(length);
      return result;
    }

    /// <summary>
    /// Returns the concatenated list of <paramref name="values"/>.
    /// </summary>
    /// <remarks>
    /// Used by extension methods.
    /// </remarks>
    public string Concat(string[] values)
    {
      return string.Concat(values);
    }

    /// <summary>
    /// Returns <paramref name="value"/>.
    /// </summary>
    /// <remarks>
    /// Used by extension methods.
    /// </remarks>
    public string Echo(string value)
    {
      return value;
    }
  }
}
