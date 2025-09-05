namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of last names.
  /// </summary>
  public sealed class LastNameGenerator : ResourceFileStringGenerator
  {
    /// <summary>
    /// Initializes this instance with a list of last names.
    /// </summary>
    public LastNameGenerator() : base("LastNames") { }
  }
}
