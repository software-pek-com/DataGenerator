using DataGenerator.Core;

/// <summary>
/// Represents extension methods for <see cref="IComplexGenerator">complex generators</see>.
/// </summary>
public static class IComplexGeneratorExtensions
{
  /// <summary>
  /// Generates a number of new objects.
  /// </summary>
  public static IEnumerable<T> Generate<T>(this IComplexGenerator<T> generator, int count) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(-1, count, nameof(count));

    var objs = new List<T>(count);
    for (int i = 0; i < count; ++i)
    {
      objs.Add(generator.Generate());
    }

    return objs;
  }

  /// <summary>
  /// Generates a new object with a finalization action.
  /// </summary>
  public static T Generate<T>(this IComplexGenerator<T> generator, Action<T> finalizeAction) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(finalizeAction, nameof(finalizeAction));

    var finalizer = new GeneratorFinalizer<T>(finalizeAction);

    generator.Finalize += finalizer;

    return generator.Generate();
  }

  /// <summary>
  /// Generates a number of new objects with a finalization action.
  /// </summary>
  public static IEnumerable<T> Generate<T>(this IComplexGenerator<T> generator, int count, Action<T> finalizeAction) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(0, count, nameof(count));
    Guard.ArgumentNotNull(finalizeAction, nameof(finalizeAction));

    var finalizer = new GeneratorFinalizer<T>(finalizeAction);

    generator.Finalize += finalizer;

    var objs = generator.Generate(count);

    return objs;
  }

  /// <summary>
  /// Generate distinct values in the range [1 .. <paramref name="maxNumberOfItems"/>]
  /// </summary>
  /// <typeparam name="T">The type of value to generate.</typeparam>
  /// <param name="generator">The generator.</param>
  /// <param name="comparer">The comparer.</param>
  /// <param name="maxNumberOfItems">The maximum number of items. It must be at least 1.</param>
  /// <returns>The distinct generated values.</returns>
  public static IEnumerable<T> GenerateDistinctInRange<T>(this IComplexGenerator<T> generator, IEqualityComparer<T> comparer, int maxNumberOfItems)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(comparer, nameof(comparer));
    Guard.ArgumentBigger(0, maxNumberOfItems, nameof(maxNumberOfItems));

    var random = new Random();
    int numberOfItems = random.Next(1, maxNumberOfItems + 1);

    var items = new List<T>();
    for (int i = 0; i < numberOfItems; ++i)
    {
      var item = generator.Generate();
      items.Add(item);
    }

    var distinctItems = items.Distinct(comparer);
    return distinctItems.ToList();
  }
}
