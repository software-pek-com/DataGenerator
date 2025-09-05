using DataGenerator.Core;
using DataGenerator.Services;
using System.Reflection;

namespace DataGenerator.Generators
{
  /// <summary>
  /// This generator provides instances of <typeparam name="T"/> deserialized from a JSON resource file.
  /// </summary>
  public class JsonResourceFileGenerator<T> : DataSourceValueGenerator<T>
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public JsonResourceFileGenerator(Assembly type, string resourceFileName)
      : base(new JsonResourceFileDataSource<T>(type, resourceFileName)) { }
  }
}
