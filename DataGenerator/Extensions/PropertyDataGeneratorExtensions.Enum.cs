using DataGenerator.Core;
using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the <see cref="PropertyDataGenerator{T,TResult}"/> class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a new enum value (<typeparamref name="TEnum"/>) for the given property value.
  /// </summary>
  /// <typeparam name="T">The type of the owner object being generated</typeparam>
  /// <typeparam name="TEnum">The type of the enum property being generated</typeparam>
  /// <param name="generator">The generator being extended; may not be null</param>
  public static void AsEnum<T, TEnum>(this PropertyDataGenerator<T, TEnum> generator)
      where T : class, new()
      where TEnum : struct
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    var enumGenerator = new EnumGenerator<TEnum>();

    generator.RegisterValueGenerator(enumGenerator);
  }

  /// <summary>
  /// Generates a new enum value (<typeparamref name="TEnum"/>) for the given property value excluding
  /// enum values in <paramref name="blacklist"/>.
  /// </summary>
  /// <typeparam name="T">The type of the owner object being generated</typeparam>
  /// <typeparam name="TEnum">The type of the enum property being generated</typeparam>
  /// <param name="generator">The generator being extended; may not be null</param>
  /// <param name="blacklist">The black-listed enum values; may not be null</param>
  public static void AsEnumExcept<T, TEnum>(this PropertyDataGenerator<T, TEnum> generator, IEnumerable<TEnum> blacklist)
      where T : class, new()
      where TEnum : struct
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(blacklist, nameof(blacklist));

    var enumGenerator = new EnumGenerator<TEnum>(blacklist);

    generator.RegisterValueGenerator(enumGenerator);
  }

  /// <summary>
  /// Generates a new enum value (<typeparamref name="TEnum"/>) with equal probability, or null with the given probability, for the given property value.
  /// </summary>
  /// <typeparam name="T">The type of the owner object being generated</typeparam>
  /// <typeparam name="TEnum">The non-nullable base type of the nullable enum property being generated</typeparam>
  /// <param name="generator">The generator being extended; may not be null</param>
  /// <param name="probabilityOfNull">The probability of generating a null value; must be in the range [0.0, 1.0]</param>
  public static void AsEnumOrNull<T, TEnum>(this PropertyDataGenerator<T, TEnum?> generator, double probabilityOfNull)
      where T : class, new()
      where TEnum : struct
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, probabilityOfNull, nameof(probabilityOfNull));

    var nonNullableEnumGenerator = new EnumGenerator<TEnum>();

    generator.RegisterValueGenerator(nonNullableEnumGenerator, probabilityOfNull);
  }
}
