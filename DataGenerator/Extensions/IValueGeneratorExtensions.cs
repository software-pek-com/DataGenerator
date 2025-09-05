using DataGenerator.Core;

/// <summary>
/// Represents extension methods for <see cref="IValueGenerator"/>
/// </summary>
public static class IValueGeneratorExtensions
{
  /// <summary>
  /// Returns a list of instances of type <typeparamref name="T"/>.
  /// </summary>
  public static List<T> NewList<T>(this IValueGenerator<T> generator, int count)
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(0, count, nameof(count));

    var valueList = new List<T>(count);
    for (int i = 0; i < count; ++i)
    {
      var value = generator.New();
      valueList.Add(value);
    }

    return valueList;
  }
}
