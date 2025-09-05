using DataGenerator.Core;
using DataGenerator.Extensions;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a format pattern descriptor for text generators.
  /// A format pattern is as per the string.Format method.
  /// </summary>
  public sealed class FormatDescriptor
  {
    private string? _FormatPattern;

    /// <summary>
    /// Constructor.
    /// </summary>
    public FormatDescriptor() { }

    /// <summary>
    /// Initializes this instance with a format pattern.
    /// </summary>
    public FormatDescriptor(string pattern)
    {
      Guard.ArgumentNotNullOrWhitespace(pattern, nameof(pattern));

      Pattern = pattern;
    }

    /// <summary>
    /// Gets or sets the format pattern used to generate text using this context.
    /// </summary>
    public string Pattern
    {
      get { return _FormatPattern; }
      set
      {
        Guard.ArgumentNotNullOrEmpty(value, nameof(value));

        if (_FormatPattern == value) { return; }

        var parameterCount = value.GetFormatParameterCount();

        if (parameterCount == 0)
        {
          throw new InvalidOperationException(
            $"Supplied format '{value}' has no parameters");
        }

        ParameterCount = parameterCount;
        _FormatPattern = value;
      }
    }

    /// <summary>
    /// Gets the count of format parameters found in the FormatPattern.
    /// </summary>
    /// <remarks>
    /// Uses the string extension method 'GetFormatParameterCount' to count the parameters.
    /// </remarks>
    public int ParameterCount { get; private set; }
  }
}
