using DataGenerator.Core;
using Newtonsoft.Json;
using System.Reflection;

namespace DataGenerator.Services
{
  /// <summary>
  /// This class represents a data source reading a list of instances of <typeparam name="T"/> from a resource file.
  /// </summary>
  public class JsonResourceFileDataSource<T> : IValueDataSource<T>
  {
    private readonly Lazy<IEnumerable<T>> _Items;

    /// <summary>
    /// Constructor.
    /// </summary>
    public JsonResourceFileDataSource(Assembly assembly, string jsonFileName)
    {
      Guard.ArgumentNotNull(assembly, nameof(assembly));
      Guard.ArgumentNotNullOrWhitespace(jsonFileName, nameof(jsonFileName));

      _Items = new Lazy<IEnumerable<T>>(() => LazyInitialize(assembly, jsonFileName));
    }

    /// <summary>
    /// Gets all the data source's values.
    /// </summary>
    /// <returns>All the values.</returns>
    public IEnumerable<T> GetAllValues()
    {
      return _Items.Value;
    }

    private static IEnumerable<T> LazyInitialize(Assembly assembly, string jsonFileName)
    {
      var fullyQualifiedResourceFileName = assembly.GetName().Name + ".Resources." + jsonFileName;

      if (!assembly.GetManifestResourceNames().Any(r => r == fullyQualifiedResourceFileName))
      {
        var message = "Embedded resource file with name '{0}' was not found in 'Resources' directory of assembly '{1}'.";
        message += " Please make sure the file name is correct and the file is set as embedded resource.";
        throw new InvalidOperationException(string.Format(message, jsonFileName, assembly.GetName().Name));
      }

      var serializer = new JsonSerializer();
      using (var stream = assembly.GetManifestResourceStream(fullyQualifiedResourceFileName))
      {
        var reader = new StreamReader(stream!);
        var jsonReader = new JsonTextReader(reader);

        return serializer.Deserialize<T[]>(jsonReader)!;
      }
    }
  }
}
