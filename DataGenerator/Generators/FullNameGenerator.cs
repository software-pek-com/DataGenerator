namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of full names (first and last name).
  /// </summary>
  public class FullNameGenerator : FormattedStringGenerator
  {
    /// <summary>
    /// Initializes this instance with a list of last names.
    /// </summary>
    public FullNameGenerator()
        : base("{0} {1}")
    {
      RegisterValues(0, new ResourceFileStringGenerator(["FemaleFirstNames", "MaleFirstNames"]));
      RegisterValues(1, new ResourceFileStringGenerator("LastNames"));
    }
  }
}
