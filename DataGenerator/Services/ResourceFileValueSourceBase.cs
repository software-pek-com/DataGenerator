using DataGenerator.Core;

namespace DataGenerator.Services
{
  /// <summary>
  /// This class represents a data source, reading its values of type <typeparam name="T"/> from a resource file.
  /// </summary>
  public abstract class ResourceFileDataSourceBase<T> : IValueDataSource<T>
  {
    private readonly List<string> _ResourceFileNames;

    /// <summary>
    /// Internal for unit tests.
    /// </summary>
    internal readonly Lazy<List<T>> _Items;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileName">The name of the resource file to be read.</param>
    public ResourceFileDataSourceBase(string resourceFileName)
    {
      Guard.ArgumentNotNullOrWhitespace(resourceFileName, nameof(resourceFileName));

      _ResourceFileNames = new List<string> { resourceFileName };
      _Items = new Lazy<List<T>>(() => LazyInitialize(AssemblyInfo.Type));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">A type from the module where resource file is located.</param>
    /// <param name="resourceFileName">The names of the resource files containing the strings.</param>
    public ResourceFileDataSourceBase(Type type, string resourceFileName)
    {
      Guard.ArgumentNotNull(type, nameof(type));
      Guard.ArgumentNotNullOrWhitespace(resourceFileName, nameof(resourceFileName));

      _ResourceFileNames = new List<string> { resourceFileName };
      _Items = new Lazy<List<T>>(() => LazyInitialize(type));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceFileNames">The names of the resource files to be read.</param>
    public ResourceFileDataSourceBase(string[] resourceFileNames)
    {
      Guard.ArgumentNotNull(resourceFileNames, nameof(resourceFileNames));
      Guard.ArgumentNotNullOrEmpty(resourceFileNames, nameof(resourceFileNames));
      foreach (var resourceFileName in resourceFileNames)
      {
        Guard.ArgumentNotNullOrWhitespace(resourceFileName, nameof(resourceFileName));
      }

      _ResourceFileNames = resourceFileNames.ToList();
      _Items = new Lazy<List<T>>(() => LazyInitialize(AssemblyInfo.Type));
    }

    /// <summary>
    /// Gets all the data source's values.
    /// </summary>
    /// <returns>All the values.</returns>
    public IEnumerable<T> GetAllValues()
    {
      return _Items.Value;
    }

    /// <summary>
    /// Gets the first <param name="count"/> items of source's values.
    /// </summary>
    public List<T> GetValues(int count)
    {
      Guard.ArgumentBigger(0, count, nameof(count));

      var items = _Items.Value;
      var toTake = count;

      if (count > items.Count) { toTake = items.Count; }

      var result = items.Take(toTake);

      return result.ToList();
    }

    /// <summary>
    /// Adapt a string item to the type provided by the data source.
    /// </summary>
    internal abstract T AdaptItem(string item);

    private List<T> LazyInitialize(Type type)
    {
      var allStringItems = new List<string>();

      foreach (string resourceFileName in _ResourceFileNames)
      {
        var text = ResourceHelper.Singleton.GetText(type, resourceFileName);
        var stringItems = GetStringItems(text);
        allStringItems.AddRange(stringItems);
      }

      return allStringItems.Select(AdaptItem).ToList();
    }

    /// <summary>
    /// Splits the given input string and returns string[] filled with the items.
    /// </summary>
    /// <remarks>
    /// Protected for unit tests
    /// </remarks>
    protected static string[] GetStringItems(string text)
    {
      var stringItems = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

      var trimmedItems = stringItems.Select(s => s.Trim()).ToArray();

      return trimmedItems;
    }
  }
}
