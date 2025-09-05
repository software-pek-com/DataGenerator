namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of street names.
  /// </summary>
  /// <remarks>
  /// Not sealed for unit tests only.
  /// </remarks>
  public class StreetNameGenerator : FormattedStringGenerator
  {
    private static readonly List<string> _StreetQualifiers =
    [
      "Road", "Street", "Avenue", "Place", "Drive", "Walk", "Boulevard"
    ];

    /// <summary>
    /// Initializes this instance with a list of street names.
    /// </summary>
    public StreetNameGenerator()
        : base("{0} {1}")
    {
      RegisterValues(0, new ResourceFileStringGenerator("StreetNames"));

      RegisterValues(1, _StreetQualifiers);
    }
  }
}
