using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DataGenerator.Core
{
  /// <summary>
  /// Represents a helper class for managing shared resources.
  /// </summary>
  public class ResourceHelper
  {
    /// <summary>
    /// Returns the text resource with the given containing name or being equal to the name.
    /// </summary>
    /// <param name="source">The object whose assembly will be used to locate the indicated resource.</param>
    /// <param name="resourceNamePortion">
    /// A portion of the resource name.
    /// It is derived from the file name of the resource.
    /// The full resource name which follows the following pattern: NameSpace.Filename (e.g Namespace.SomeFile.txt).
    /// </param>
    public string GetText(object source, string resourceNamePortion)
    {
      Guard.ArgumentNotNull(source, nameof(source));
      Guard.ArgumentNotNullOrWhitespace(resourceNamePortion, nameof(resourceNamePortion));

      return GetText(source.GetType().Assembly, resourceNamePortion);
    }

    /// <summary>
    /// Returns the text resource with the given containing name or being equal to the name.
    /// </summary>
    /// <param name="type">The type whose assembly will be used to locate the indicated resource.</param>
    /// <param name="resourceNamePortion">
    /// A portion of the resource name.
    /// It is derived from the file name of the resource.
    /// The full resource name which follows the following pattern: NameSpace.Filename.
    /// </param>
    public string GetText(Type type, string resourceNamePortion)
    {
      Guard.ArgumentNotNull(type, nameof(type));
      Guard.ArgumentNotNullOrWhitespace(resourceNamePortion, nameof(resourceNamePortion));

      return GetText(type.Assembly, resourceNamePortion);
    }

    /// <summary>
    /// Returns the text resource with the given containing name.
    /// </summary>
    /// <param name="assembly">The assembly containing the resource.</param>
    /// <param name="resourceNamePortion">
    /// A portion of the resource name.
    /// It is derived from the file name of the resource.
    /// The full resource name which follows the following pattern: NameSpace.Filename.
    /// </param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="InvalidOperationException">Resource name was not found</exception>
    public static string GetText(Assembly assembly, string resourceNamePortion)
    {
      Guard.ArgumentNotNull(assembly, nameof(assembly));
      Guard.ArgumentNotNullOrWhitespace(resourceNamePortion, nameof(resourceNamePortion));

      var fullResourceName = GetFullResourceName(assembly, resourceNamePortion);
      using var reader = new StreamReader(GetResourceStream(assembly, fullResourceName));
      return reader.ReadToEnd();
    }

    /// <summary>
    /// Returns the resource with the given containing name as bytes.
    /// </summary>
    /// <param name="assembly">The assembly containing the resource.</param>
    /// <param name="resourceNamePortion">
    /// A portion of the resource name.
    /// It is derived from the file name of the resource.
    /// The full resource name which follows the following pattern: NameSpace.Filename.
    /// </param>
    /// <returns>The bytes of the resource.</returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="InvalidOperationException">Resource name was not found.</exception>
    public static byte[] GetBytes(Assembly assembly, string resourceNamePortion)
    {
      Guard.ArgumentNotNull(assembly, nameof(assembly));
      Guard.ArgumentNotNullOrWhitespace(resourceNamePortion, nameof(resourceNamePortion));

      var fullResourceName = GetFullResourceName(assembly, resourceNamePortion);
      using Stream resourceStream = GetResourceStream(assembly, fullResourceName);
      using (var memoryStream = new MemoryStream())
      {
        resourceStream.CopyTo(memoryStream);
        byte[] result = memoryStream.ToArray();
        return result;
      }
    }

    /// <summary>
    /// Tries to return the resource set for an assembly.
    /// </summary>
    public static ResourceSet TryGetResourceSet(Assembly assembly)
    {
      Guard.ArgumentNotNull(assembly, nameof(assembly));

      try
      {
        var objResourceManager = new ResourceManager(assembly.GetName().Name + ".g", assembly);
        var resourceSet = objResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true)!;

        return resourceSet;
      }
      catch (MissingManifestResourceException exception)
      {
        Trace.WriteLine($"Failed to get resource set for '{assembly.FullName}':\n{exception}");

        return null!;
      }
    }

    private static Stream GetResourceStream(Assembly assembly, string resourceName)
    {
      var result = assembly.GetManifestResourceStream(resourceName)!;
      return result;
    }

    private static string GetFullResourceName(Assembly assembly, string resourceNamePortion)
    {
      var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(r => r == resourceNamePortion);
      if (fullResourceName == null)
      {
        fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(r => r.Contains(resourceNamePortion));
      }

      if (fullResourceName == null)
      {
        var errorMessage = $"The resource name '{resourceNamePortion}' was not found in assembly '{assembly}'.";
        throw new InvalidOperationException(errorMessage);
      }

      return fullResourceName;
    }

    #region Singleton

    private static readonly Lazy<ResourceHelper> singleton = new(true);

    /// <summary>
    /// Gets the singleton instance of the resource helper.
    /// </summary>
    public static ResourceHelper Singleton
    {
      get { return singleton.Value; }
    }

    #endregion
  }
}
