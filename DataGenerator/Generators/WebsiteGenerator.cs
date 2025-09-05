namespace DataGenerator.Generators
{
  /// <summary>
  /// Generator that generates website addresses.
  /// </summary>
  public class WebsiteGenerator : FormattedStringGenerator
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public WebsiteGenerator()
        : base("www.{0}.{1}")
    {
      RegisterValues(0, new StringGenerator(1, 10));
      RegisterValues(1, new ResourceFileStringGenerator("WebsiteExtensions"));
    }
  }
}
