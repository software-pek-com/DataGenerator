using DataGenerator;
using DataGenerator.Core;
using DataGenerator.Extensions;
using DataGenerator.Generators;
using System.Linq.Expressions;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  /// <summary>
  /// Generates a string of the given length.
  /// </summary>
  public static void OfLength<T>(this PropertyDataGenerator<T, string> generator, int length)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_StringGenerator.Value, "OfLength", 0.0, length);
  }

  /// <summary>
  /// Generates a string of the given length or null with given probability.
  /// </summary>
  public static void OfLengthOrNull<T>(this PropertyDataGenerator<T, string> generator, double nullProbability, int length)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));
    generator.RegisterValueGenerator(_StringGenerator.Value, "OfLength", nullProbability, length);
  }

  /// <summary>
  /// Generates a random first name for the given property value.
  /// </summary>
  public static void AsFirstName<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_FirstNameGenerator.Value);
  }

  /// <summary>
  /// Generates a first name (female if isFemale is true, male otherwise) for the given property value.
  /// </summary>
  public static void AsFirstName<T>(this PropertyDataGenerator<T, string> generator, bool isFemale)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));

    if (isFemale)
    {
      generator.RegisterValueGenerator(_FemaleFirstNameGenerator.Value);
    }
    else
    {
      generator.RegisterValueGenerator(_MaleFirstNameGenerator.Value);
    }
  }

  /// <summary>
  /// Generates a first name for the given property value with the probability of it being a female name.
  /// </summary>
  public static void AsFirstName<T>(this PropertyDataGenerator<T, string> generator, double femaleProbability)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentInRange(0.0, 1.0, femaleProbability, nameof(femaleProbability));

    // We re-use the null generate method to generate the probability of a female name.
    bool isFemale = _RandomNumber.Value.IsNextNull(femaleProbability);
    AsFirstName<T>(generator, isFemale);
  }

  /// <summary>
  /// Generates a last name for the given property value.
  /// </summary>
  public static void AsLastName<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_LastNameGenerator.Value);
  }

  /// <summary>
  /// Generates a full name (first and last names) for the given property value.
  /// </summary>
  public static void AsFullName<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_FullNameGenerator.Value);
  }

  /// <summary>
  /// Generates a username name for the given property value.
  /// </summary>
  public static void AsUsername<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_UsernameGenerator.Value);
  }

  /// <summary>
  /// Generates a name for the given property value.
  /// </summary>
  public static void AsName<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_NamesGenerator.Value);
  }

  /// <summary>
  /// Generates a salutation for the given property value.
  /// </summary>
  public static void AsSalutation<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_SalutationGenerator.Value);
  }

  /// <summary>
  /// Generates a username name for the given property value.
  /// </summary>
  public static void AsUsername<T>(this PropertyDataGenerator<T, string> generator, int length)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentBigger(0, length, nameof(length));
    generator.RegisterValueGenerator(_UsernameGenerator.Value, "NewUsername", 0.0, new object[] { length });
  }

  /// <summary>
  /// Gets a random country.
  /// </summary>
  public static void AsCountry<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_CountryNameGenerator.Value);
  }

  /// <summary>
  /// Gets a random phone number.
  /// </summary>
  public static void AsPhoneNumber<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_PhoneNumberGenerator.Value);
  }

  /// <summary>
  /// Gets a random IBAN.
  /// </summary>
  public static void AsIban<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_IbanGenerator.Value);
  }

  /// <summary>
  /// Gets a random website.
  /// </summary>
  public static void AsWebsite<T>(this PropertyDataGenerator<T, string> generator)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    generator.RegisterValueGenerator(_WebsiteGenerator.Value);
  }

  /// <summary>
  /// Gets a random city based on a country.
  /// </summary>
  public static void AsCity<T>(this PropertyDataGenerator<T, string> generator, Expression<Func<T, string?>> countryProperty)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(countryProperty, nameof(countryProperty));

    var parameters = new[] { countryProperty };

    generator.RegisterValueGenerator(_CityGenerator.Value, "NewFromCountry", parameters);
  }

  /// <summary>
  /// Gets a random string from a resource file given property value.
  /// </summary>
  public static void AsResourceFileString<T>(this PropertyDataGenerator<T, string> generator, string resourceFileName)
      where T : class, new()
  {
    generator.AsResourceFileString(AssemblyInfo.Type, resourceFileName);
  }

  /// <summary>
  /// Gets a random string from a resource file given property value.
  /// </summary>
  public static void AsResourceFileString<T>(this PropertyDataGenerator<T, string> generator, Type type, string resourceFileName)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(type, nameof(type));
    Guard.ArgumentNotNullOrEmpty(resourceFileName, nameof(resourceFileName));

    if (!_ResourceFileStringGenerators.TryGetValue(resourceFileName, out ResourceFileStringGenerator? resourceFileStringGenerator))
    {
      resourceFileStringGenerator = new ResourceFileStringGenerator(type, resourceFileName);
      _ResourceFileStringGenerators[resourceFileName] = resourceFileStringGenerator;
    }

    generator.RegisterValueGenerator(resourceFileStringGenerator);
  }

  /// <summary>
  /// Uses expression to generate.
  /// </summary>
  public static void As<T>(this PropertyDataGenerator<T, string> generator, Expression<Func<T, string>> propertyExpression)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(propertyExpression, nameof(propertyExpression));

    var parameters = new[] { propertyExpression };

    generator.RegisterValueGenerator(_StringGenerator.Value, "Echo", parameters);
  }

  /// <summary>
  /// Uses expression to generate.
  /// </summary>
  public static void As<T>(this PropertyDataGenerator<T, string> generator, Expression<Func<T, string[]>> propertyExpression)
      where T : class, new()
  {
    Guard.ArgumentNotNull(generator, nameof(generator));
    Guard.ArgumentNotNull(propertyExpression, nameof(propertyExpression));

    var parameters = new[] { propertyExpression };

    generator.RegisterValueGenerator(_StringGenerator.Value, "Concat", parameters);
  }
}
