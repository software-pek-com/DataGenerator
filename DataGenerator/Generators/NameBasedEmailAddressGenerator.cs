using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of email addresses which use provided first and last names.
  /// </summary>
  /// <remarks>
  /// <para/><see cref="New"/> throws an exception because it is not supposed to be used. The class implements
  /// <see cref="IValueGenerator"/> to ensure it can be used within the fluent API.
  /// </remarks>
  public class NameBasedEmailAddressGenerator : IValueGenerator
  {
    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal readonly List<string> _EmailDomains;

    /// <summary>
    /// Constructor.
    /// </summary>
    public NameBasedEmailAddressGenerator()
    {
      var emailProvidersText = ResourceHelper.Singleton.GetText(AssemblyInfo.Type, "EmailProviders");
      _EmailDomains = emailProvidersText.Split('\n').ToList();
    }

    /// <summary>
    /// Generates a new email address using the given first and last names.
    /// </summary>
    public string NewEmailAddressFromSplitName(string firstName, string lastName)
    {
      Guard.ArgumentNotNullOrEmpty(firstName, nameof(firstName));
      Guard.ArgumentNotNullOrEmpty(lastName, nameof(lastName));

      var generator = new InnerGenerator(firstName, lastName, _EmailDomains);
      return (string)generator.New();
    }

    /// <summary>
    /// Generates a new email address using the given full name.
    /// The space characters are replaced by dots.
    /// </summary>
    public string NewEmailAddressFromFullName(string fullName)
    {
      Guard.ArgumentNotNullOrEmpty(fullName, nameof(fullName));

      var generator = new InnerGenerator(fullName, _EmailDomains);
      return (string)generator.New();
    }

    /// <summary>
    /// Throws <see cref="NotImplementedException"/>.
    /// </summary>
    public object New()
    {
      throw new NotImplementedException();
    }

    private class InnerGenerator : FormattedStringGenerator
    {
      /// <summary>
      /// Initializes this instance with a first and last name generators.
      /// </summary>
      public InnerGenerator(string firstName, string lastName, List<string> emailProviders)
        : base("{0}.{1}@{2}")
      {
        RegisterValues(0, new List<string> { firstName });
        RegisterValues(1, new List<string> { lastName });
        RegisterValues(2, emailProviders);
      }

      /// <summary>
      /// Initializes this instance with a first and last name generators.
      /// </summary>
      public InnerGenerator(string fullName, List<string> emailProviders)
        : base("{0}@{1}")
      {
        RegisterValues(0, new List<string> { fullName.Replace(' ', '.') });
        RegisterValues(1, emailProviders);
      }
    }
  }
}
