using DataGenerator.Core;
using DataGenerator.Generators;
using System.Linq.Expressions;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a random email address.
  /// </summary>
  public static void AsEmailAddress<T>(this PropertyDataGenerator<T, string> generator) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    generator.RegisterValueGenerator(_RandomEmailAddressGenerator.Value);
  }

  /// <summary>
  /// Generates an email address using the given properties as arguments.
  /// </summary>
  public static void AsEmailAddress<T>(this PropertyDataGenerator<T, string> generator,
      Expression<Func<T, string>> firstNameProperty,
      Expression<Func<T, string>> lastNameProperty) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(firstNameProperty, nameof(firstNameProperty));
    Guard.ArgumentNotNull(lastNameProperty, nameof(lastNameProperty));

    var parameters = new[] { firstNameProperty, lastNameProperty };

    generator.RegisterValueGenerator(_NameBasedEmailAddressGenerator.Value, "NewEmailAddressFromSplitName", parameters);
  }

  /// <summary>
  /// Generates an email address using the given properties as arguments.
  /// </summary>
  public static void AsEmailAddress<T>(this PropertyDataGenerator<T, string> generator,
      Expression<Func<T, string>> fullNameProperty) where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(fullNameProperty, nameof(fullNameProperty));

    var parameters = new[] { fullNameProperty };

    generator.RegisterValueGenerator(_NameBasedEmailAddressGenerator.Value, "NewEmailAddressFromFullName", parameters);
  }
}
