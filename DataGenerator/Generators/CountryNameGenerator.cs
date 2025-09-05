namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of country names.
  /// </summary>
  public sealed class CountryNameGenerator : ResourceFileStringGenerator
  {
    /// <summary>
    /// Initializes this instance with a list of countries.
    /// </summary>
    public CountryNameGenerator() : base("Countries") { }
  }
}
