using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a birth date for the given property value.
  /// The date generated will be for a working age person i.e. between 18 and 65 years old.
  /// </summary>
  public static void AsBirthDate<T>(this PropertyDataGenerator<T, DateTime> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_DateTimeGenerator.Value, "NewBirthDate");
  }

  /// <summary>
  /// Generates a birth date for the given property value.
  /// The date generated will be for a working age person i.e. between 18 and 65 years old.
  /// </summary>
  public static void AsBirthDateOrNull<T>(this PropertyDataGenerator<T, DateTime?> generator, double nullProbability)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

    generator.RegisterValueGenerator(_DateTimeGenerator.Value, "NewBirthDate", nullProbability);
  }

  /// <summary>
  /// Generates a date time for the given property value.
  /// </summary>
  public static void AsDateTime<T>(this PropertyDataGenerator<T, DateTime> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_DateTimeGenerator.Value);
  }

  /// <summary>
  /// Generates a datetime value in the specified range i.e [from, to).
  /// Note 'max' is not included in the range.
  /// </summary>
  public static void AsDateTime<T>(this PropertyDataGenerator<T, DateTime> generator, DateTime from, DateTime to)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    if (from > to)
    {
      throw new ArgumentException("The maxValue must be bigger or equal than the minValue.");
    }

    generator.RegisterValueGenerator(_DateTimeGenerator.Value, "InRange", 0.0, from, to);
  }

  /// <summary>
  /// Generates a date time (or null with a probability) for the given property value.
  /// </summary>
  public static void AsDateTimeOrNull<T>(this PropertyDataGenerator<T, DateTime?> generator, double nullProbability)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

    generator.RegisterValueGenerator(_DateTimeGenerator.Value, nullProbability);
  }

  /// <summary>
  ///Generates a datetime value in the specified range i.e [from, to) (or null with a probability) for the given property value.
  /// </summary>
  public static void AsDateTimeOrNull<T>(this PropertyDataGenerator<T, DateTime?> generator, double nullProbability, DateTime from, DateTime to)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

    generator.RegisterValueGenerator(_DateTimeGenerator.Value, "InRange", nullProbability, from, to);
  }
}
