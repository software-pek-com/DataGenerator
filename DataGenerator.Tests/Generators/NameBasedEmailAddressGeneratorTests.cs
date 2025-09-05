using DataGenerator.Generators;
using System.Text.RegularExpressions;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class NameBasedEmailAddressGeneratorTests
  {
    private static readonly Regex _EmailValidationRegex =
        new Regex(@"^(?<user>[^@]+)@(?<host>.+)\z", RegexOptions.Compiled);

    /// <summary>
    /// Represents a simple email address format validator.
    /// </summary>
    private static bool IsValidEmailFormat(string emailAddress)
    {
      var match = _EmailValidationRegex.Match(emailAddress);
      return match.Success;
    }

    private class NameBasedEmailAddressGeneratorMock : NameBasedEmailAddressGenerator
    {
      public List<string> EmailDomainNames { get { return _EmailDomains; } }
    }

    [Fact]
    public void Constructor()
    {
      var target = new NameBasedEmailAddressGeneratorMock();

      Assert.NotNull(target);
      Assert.NotNull(target.EmailDomainNames);
      Assert.True(target.EmailDomainNames.Count > 0);
    }

    [Fact]
    public void NewValue()
    {
      var target = new NameBasedEmailAddressGenerator();

      Assert.Throws<NotImplementedException>(() => target.New());
    }

    [Fact]
    public void NewEmailAddress()
    {
      var target = new NameBasedEmailAddressGenerator();

      var result = target.NewEmailAddressFromSplitName("X123", "Y321");

      Assert.NotNull(result);
      Assert.Contains("X123", result);
      Assert.Contains("Y321", result);
      Assert.True(IsValidEmailFormat(result));
    }
  }
}
