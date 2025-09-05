namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of first names.
  /// </summary>
  public sealed class FirstNameGenerator : ResourceFileStringGenerator
  {
    /// <summary>
    /// Initializes this instance with a list of male or female first names.
    /// </summary>
    public FirstNameGenerator(bool isFemale)
      : base(isFemale ? "FemaleFirstNames" : "MaleFirstNames")
    {
    }

    /// <summary>
    /// Initializes this instance with a list of male and female first names.
    /// </summary>
    public FirstNameGenerator()
      : base([ "FemaleFirstNames", "MaleFirstNames" ])
    {
    }
  }
}
