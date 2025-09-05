using DataGenerator.Core;
using System.Text;
using System.Text.RegularExpressions;

namespace DataGenerator.Extensions
{
  /// <summary>
  /// Represents helper methods for <see cref="string"/>s.
  /// </summary>
  public static class StringExtensions
  {
    private static readonly Lazy<Random> randomNumber = new(true);

    private static readonly Lazy<Regex> parameterFormatRegex = new(() => new Regex(@"(?<!\{)(?>\{\{)*\{\d(.*?)"));

    /// <summary>
    /// Returns a random string of the given length.
    /// </summary>
    public static string GenerateRandom(int length)
    {
      // str can be null as it is ignored anyway - this is a 'static' method.
      Guard.ArgumentBigger(-1, length, nameof(length));

      var sb = new StringBuilder();
      int ascii;
      for (int i = 0; i < length; ++i)
      {
        ascii = Convert.ToInt32(Math.Floor(26 * randomNumber.Value.NextDouble() + 65));
        sb.Append(Convert.ToChar(ascii));
      }

      return sb.ToString();
    }

    /// <summary>
    /// Returns the count of expected parameters (args) in a format string as used by string.Format.
    /// <para/>- It returns only the number of unique parameters e.g for pattern "{0}-{1}-{0}" returns 2.
    /// <para/>- It handles cases where there are escaped bracket sequences e.g. for "{{4}}" returns 0.
    /// <para/>- It does not handle mistakes in the format string where a parameter is
    /// missing e.g. for "{0} {3} {5}" returns 3.
    /// </summary>
    /// <remarks>
    /// Implementation reference: http://stackoverflow.com/questions/4989106/string-format-count-the-number-of-expected-args
    /// </remarks>
    public static int GetFormatParameterCount(this string format)
    {
      Guard.ArgumentNotNull(format, nameof(format));

      var matches = parameterFormatRegex.Value.Matches(format);
      var uniqueMatchCount = matches.OfType<Match>().Select(m => m.Value).Distinct().Count();

      return matches.Count;
    }

    /// <summary>
    /// Returns the string with a capitalized first letter.
    /// </summary>
    public static string FirstToUpper(this string source)
    {
      if (string.IsNullOrEmpty(source))
      {
        return string.Empty;
      }

      if (Char.IsUpper(source, 0))
      {
        return source;
      }

      // Convert to char array of the string
      var letters = source.ToCharArray();

      // Upper case the first char
      letters[0] = char.ToUpper(letters[0]);

      // Return the array made of the new char array
      return new string(letters);
    }
  }
}