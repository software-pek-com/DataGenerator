namespace DataGenerator.Services
{
  /// <summary>
  /// This class represents a data source reading its string values from a resource file.
  /// </summary>
  public class StringResourceFileDataSource : ResourceFileDataSourceBase<string>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileName">The name of the resource file to be read.</param>
    public StringResourceFileDataSource(string resourceFileName) : base(resourceFileName) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">A type from the module where the resource file is located.</param>
    /// <param name="resourceFileName">The name of the resource file to be read.</param>
    public StringResourceFileDataSource(Type type, string resourceFileName) : base(type, resourceFileName) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileNames">The name of the resource file to be read.</param>
    public StringResourceFileDataSource(string[] resourceFileNames) : base(resourceFileNames) { }

    /// <summary>
    /// Adapts a string item to the type provided by the data source.
    /// </summary>
    /// <remarks>Internal for unit test purpose.</remarks>
    /// <param name="item">The item to be adapted.</param>
    /// <returns>The adapted item.</returns>
    internal override string AdaptItem(string item)
    {
      return item;
    }
  }
}
