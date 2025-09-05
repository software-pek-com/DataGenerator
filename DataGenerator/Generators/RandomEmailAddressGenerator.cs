namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of email addresses.
  /// </summary>
  public class RandomEmailAddressGenerator : FormattedStringGenerator
  {
    /// <summary>
    /// Initializes this instance with given generators for first ane last names.
    /// </summary>
    public RandomEmailAddressGenerator()
        : base("{0}.{1}@{2}")
    {
      RegisterValues(0, new FirstNameGenerator());
      RegisterValues(1, new LastNameGenerator());
      RegisterValues(2, new ResourceFileStringGenerator("EmailProviders"));
    }
  }
}
