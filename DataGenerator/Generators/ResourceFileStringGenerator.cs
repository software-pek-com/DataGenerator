using DataGenerator.Core;
using DataGenerator.Services;

namespace DataGenerator.Generators
{
  /// <summary>
  /// This generator provides strings from a resource file.
  /// </summary>
  public class ResourceFileStringGenerator : DataSourceValueGenerator<string>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileName">The name of the resource file containing the strings.</param>
    public ResourceFileStringGenerator(string resourceFileName)
      : base(new StringResourceFileDataSource(resourceFileName)) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileNames">The names of the resource files containing the strings.</param>
    public ResourceFileStringGenerator(string[] resourceFileNames)
      : base(new StringResourceFileDataSource(resourceFileNames)) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">A type from the module where the resource file is located.</param>
    /// <param name="resourceFileNames">The names of the resource files containing the strings.</param>
    public ResourceFileStringGenerator(Type type, string resourceFileNames)
      : base(new StringResourceFileDataSource(type, resourceFileNames)) { }
  }
}
