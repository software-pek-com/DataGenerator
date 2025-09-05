using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DataGenerator.Core
{
  /// <summary>
  /// A static helper class that includes various parameter checking routines.
  /// </summary>
  public static class Guard
  {
    /// <summary>
    /// Throws an exception if <paramref name="argumentValue"/> is not equal to <paramref name="equalValue"/>.
    /// </summary>
    /// <exception cref="ArgumentException">When <paramref name="argumentValue"/> is not equal to <paramref name="equalValue"/>.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentEqual<T>(T equalValue, T argumentValue, string argumentName)
    {
      if (equalValue is null && argumentValue is null) { return; }

      if (argumentValue is not null && !argumentValue.Equals(equalValue))
      {
        throw new ArgumentException(
          $"'{argumentName}' value '{argumentValue}' must not be equal to '{equalValue}'.");
      }
    }

    /// <summary>
    /// Throws an exception if <paramref name="argumentValue"/> is equal to <paramref name="differentValue"/>.
    /// </summary>
    /// <exception cref="ArgumentException">When <paramref name="argumentValue"/> is equal to <paramref name="differentValue"/>.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotEqual<T>(T differentValue, T argumentValue, string argumentName)
    {
      if (differentValue is null && argumentValue is null) { return; }

      if (argumentValue is not null && argumentValue.Equals(differentValue))
      {
        throw new ArgumentException(
          $"'{argumentName}' value '{argumentValue}' must not be equal to '{differentValue}'.");
      }
    }

    /// <summary>
    /// Throws an exception if the tested string argument is null, empty or contains only white space characters.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if string value is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the string is empty or contains only white space characters.</exception>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">When argumentValue is null.</exception>
    /// <exception cref="ArgumentException">When argumentValue is empty or contains only white space characters.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotNullOrWhitespace([NotNull] string? argumentValue, string argumentName)
    {
      if (argumentValue == null) { throw new ArgumentNullException(argumentName); }
      if (string.IsNullOrWhiteSpace(argumentValue))
      {
        throw new ArgumentException($"'{argumentName}' must not be empty or contain only whitespace characters");
      }
    }

    /// <summary>
    /// Throws an exception if the tested string argument is null or the empty string.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if string value is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the string is empty</exception>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">When argumentValue is null.</exception>
    /// <exception cref="ArgumentException">When argumentValue is empty.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotNullOrEmpty([NotNull] string? argumentValue, string argumentName)
    {
      if (argumentValue == null) { throw new ArgumentNullException(argumentName); }
      if (argumentValue.Length == 0) { throw new ArgumentException($"'{argumentName}' must not be empty."); }
    }

    /// <summary>
    /// Throws an exception if the tested guid argument is the empty guid.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the guid is empty</exception>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentException">When argumentValue is empty.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotEmpty(Guid argumentValue, string argumentName)
    {
      if (argumentValue == Guid.Empty) { throw new ArgumentException($"'{argumentName}' must not be empty."); }
    }

    /// <summary>
    /// Throws an exception if the tested string argument is the empty string.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the string is empty</exception>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentException">When argumentValue is empty.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotEmpty(string argumentValue, string argumentName)
    {
      if (argumentValue.Length == 0) { throw new ArgumentException($"'{argumentName}' must not be empty."); }
    }

    /// <summary>
    /// Throws an exception if the tested string argument is empty or contains whitespace only.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the string is empty or contains whitespace only</exception>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentException">When argumentValue is empty or contains whitespace only.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotWhitespace(string argumentValue, string argumentName)
    {
      if (string.IsNullOrWhiteSpace(argumentValue)) { throw new ArgumentException($"'{argumentName}' must not be empty or contain whitespace only."); }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the given argument is null.
    /// </summary>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    /// <exception cref="ArgumentNullException">When argumentValue is null.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotNull([NotNull] object? argumentValue, string argumentName)
    {
      if (argumentValue == null) { throw new ArgumentNullException(argumentName); }
    }

    /// <summary>
    /// Throws an exception if the tested collection argument is null or empty.
    /// </summary>
    /// <param name="argumentValue">Argument values to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">When argumentValue is null.</exception>
    /// <exception cref="ArgumentNullException">If any item in the collection is null.</exception>
    /// <exception cref="ArgumentException">If any item in the collection is empty</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentNotNullOrEmpty<T>(IEnumerable<T>? argumentValue, string argumentName)
    {
      if (argumentValue is null)
      {
        throw new ArgumentNullException(argumentName);
      }

      if (!argumentValue.Any())
      {
        throw new ArgumentException($"Enumeration '{argumentName}' must not be empty.");
      }

      int i = 0;
      foreach (var argumentItemValue in argumentValue)
      {
        ArgumentNotNull(argumentItemValue, $"{argumentName}[{i++}]");
      }
    }

    /// <summary>
    /// Throws an exception if the tested string collection is null or empty, or if it contains null, empty or whitespace strings.
    /// </summary>
    /// <param name="argumentValue">Argument values to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">If argumentValue is null or any item in the collection is null.</exception>
    /// <exception cref="ArgumentException">If argumentValue is empty or any item in the collection is empty or whitespace.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentItemsNotNullOrEmpty(IEnumerable<string>? argumentValue, string argumentName)
    {
      ArgumentNotNullOrEmpty(argumentValue, argumentName);

      int i = 0;
      foreach (var argumentItemValue in argumentValue!)
      {
        ArgumentNotNullOrWhitespace(argumentItemValue, $"{argumentName}[{i++}]");
      }
    }

    /// <summary>
    /// Throws an exception if the tested string collection is null or empty, or if it contains null, empty or whitespace strings.
    /// </summary>
    /// <param name="argumentValue">Argument values to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">If argumentValue is null or any item in the collection is null.</exception>
    /// <exception cref="ArgumentException">If argumentValue is empty or any item in the collection is empty or whitespace.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentItemsNotNullOrEmpty<T>(IEnumerable<T>? argumentValue, string argumentName)
    {
      ArgumentNotNullOrEmpty(argumentValue, argumentName);

      int i = 0;
      foreach (var argumentItemValue in argumentValue!)
      {
        ArgumentNotNull(argumentItemValue, $"{argumentName}[{i++}]");
      }
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is smaller or equal to <paramref name="min"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> if tested value is smaller or equal to <paramref name="min"/>.</exception>
    /// <param name="min">The exclusive minimum of the int to throw (valid is +1).</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBigger(int min, int argumentValue, string argumentName)
    {
      if (argumentValue <= min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be > {min}.");
      }
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is smaller than <paramref name="min"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> if tested value is smaller or equal to <paramref name="min"/>.</exception>
    /// <param name="min">The inclusive minimum of the int to throw.</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBiggerOrEqual(int min, int argumentValue, string argumentName)
    {
      if (argumentValue < min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be >= {min}.");
      }
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is smaller or equal
    /// to <paramref name="min"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// if tested value is smaller or equal to <paramref name="min"/>.
    /// </exception>
    /// <param name="min">The exclusive minimum of the long to throw (valid is +1).</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBigger(long min, long argumentValue, string argumentName)
    {
      if (argumentValue <= min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be > {min}.");
      }
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is smaller than
    /// <paramref name="min"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// if tested value is smaller or equal to <paramref name="min"/>.
    /// </exception>
    /// <param name="min">The inclusive minimum of the long to throw.</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBiggerOrEqual(long min, long argumentValue, string argumentName)
    {
      if (argumentValue < min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be >= {min}.");
      }
    }

    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is smaller than <paramref name="min"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> if tested value is smaller or equal to <paramref name="min"/>.</exception>
    /// <param name="min">The inclusive minimum of the int to throw.</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBiggerOrEqual(double min, double argumentValue, string argumentName)
    {
      if (argumentValue < min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be >= {min}.");
      }
    }

    /// <summary>
    /// Overload of <see cref="ArgumentBigger(int, int, string)"/>: This is for <see cref="double"/> instead of <see cref="int"/>.
    /// </summary>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBigger(double min, double argumentValue, string argumentName)
    {
      if (argumentValue <= min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be > {min}");
      }
    }

    /// <summary>
    /// Overload of <see cref="ArgumentBigger(int, int, string)"/>: This is for <see cref="decimal"/> instead of <see cref="int"/>.
    /// </summary>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentBigger(decimal min, decimal argumentValue, string argumentName)
    {
      if (argumentValue <= min)
      {
        throw new ArgumentOutOfRangeException(argumentName, $"Value must be > {min}");
      }
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the given argument is not in the specified range (expressed as double values),
    /// i.e. &lt; <paramref name="min"/> or &gt; <paramref name="max"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> if tested value is not in the specified range.</exception>
    /// <param name="min">The minimum of the range.</param>
    /// <param name="max">The maximum of the range; is assumed to be &gt;= <paramref name="min"/>.</param>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void ArgumentInRange(double min, double max, double argumentValue, string argumentName)
    {
      if (min is double.NaN || max is double.NaN || argumentValue is double.NaN)
      {
        throw new ArgumentException("NaN is not a valid value for this operation.");
      }

      if (argumentValue < min || argumentValue > max)
      {
        string errorMessage = $"Value {argumentValue} is not in the range [{min}, {max}]";
        throw new ArgumentOutOfRangeException(argumentName, argumentValue, errorMessage);
      }
    }

    /// <summary>
    /// Verifies that an argument type is assignable from the provided type (meaning
    /// interfaces are implemented, or classes exist in the base class hierarchy).
    /// </summary>
    /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
    /// <param name="assignmentValueType">The type of the value being assigned.</param>
    /// <param name="argumentName">Argument name.</param>
    /// <exception cref="ArgumentNullException">When assignmentTargetType or assignmentValueType is null.</exception>
    /// <exception cref="ArgumentException">When target type is not assignable.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void IsOfType(Type assignmentTargetType, Type assignmentValueType, string argumentName)
    {
      IsOfType(
        assignmentTargetType,
        assignmentValueType,
        argumentName,
        $"Types are not assignable: '{assignmentTargetType}' from '{assignmentValueType}'.");
    }

    /// <summary>
    /// Verifies that an argument type is assignable from the provided type (meaning
    /// interfaces are implemented, or classes exist in the base class hierarchy).
    /// </summary>
    /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
    /// <param name="assignmentValueType">The type of the value being assigned.</param>
    /// <param name="argumentName">Argument name.</param>
    /// <param name="detailedMessage">The detailed exception message to throw when the type is not assignable</param>
    /// <exception cref="ArgumentNullException">When <paramref name="assignmentTargetType"/> or <paramref name="assignmentValueType"/> is null.</exception>
    /// <exception cref="ArgumentException">When target type is not assignable.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void IsOfType(Type assignmentTargetType, Type assignmentValueType, string argumentName, string detailedMessage)
    {
      ArgumentNullException.ThrowIfNull(assignmentTargetType);
      ArgumentNullException.ThrowIfNull(assignmentValueType);

      if (!assignmentTargetType.GetTypeInfo().IsAssignableFrom(assignmentValueType.GetTypeInfo()))
      {
        throw new ArgumentException(detailedMessage, argumentName);
      }
    }

    /// <summary>
    /// Verifies that the two types are equal
    /// </summary>
    /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
    /// <param name="assignmentValueType">The type of the value being assigned.</param>
    /// <param name="argumentName">Argument name.</param>
    /// <param name="detailedMessage">The detailed exception to throw when the type is not assignable</param>
    /// <exception cref="ArgumentNullException">When <paramref name="assignmentTargetType"/> or <paramref name="assignmentValueType"/> is null.</exception>
    /// <exception cref="ArgumentException">When target type is not assignable.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void TypeSame(Type assignmentTargetType, Type assignmentValueType, string argumentName)
    {
      ArgumentNullException.ThrowIfNull(assignmentTargetType);
      ArgumentNullException.ThrowIfNull(assignmentValueType);

      if (!ReferenceEquals(assignmentTargetType, assignmentValueType))
      {
        throw new ArgumentException(
          $"Types are not the same: '{assignmentTargetType}' and '{assignmentValueType}'.",
          argumentName);
      }
    }

    /// <summary>
    /// Verifies that the two types are equal
    /// </summary>
    /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
    /// <param name="assignmentValueType">The type of the value being assigned.</param>
    /// <param name="argumentName">Argument name.</param>
    /// <param name="detailedMessage">The detailed exception to throw when the type is not assignable</param>
    /// <exception cref="ArgumentNullException">When <paramref name="assignmentTargetType"/> or <paramref name="assignmentValueType"/> is null.</exception>
    /// <exception cref="ArgumentException">When target type is not assignable.</exception>
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static void TypeSame(Type assignmentTargetType, Type assignmentValueType, string argumentName, string detailedMessage)
    {
      ArgumentNullException.ThrowIfNull(assignmentTargetType);
      ArgumentNullException.ThrowIfNull(assignmentValueType);

      if (!ReferenceEquals(assignmentTargetType, assignmentValueType))
      {
        throw new ArgumentException(detailedMessage, argumentName);
      }
    }
  }
}
