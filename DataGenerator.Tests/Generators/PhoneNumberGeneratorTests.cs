using DataGenerator.Generators;
using System.Text.RegularExpressions;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  /// <summary>
  /// Tests for the class <see cref="PhoneNumberGenerator"/>.
  /// </summary>
  public class PhoneNumberGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      new PhoneNumberGenerator();
    }

    [Fact]
    public void New_Ok()
    {
      var target = new PhoneNumberGenerator();

      var result = target.New();

      Assert.NotNull(result);

      var regex = new Regex(@"^\+[0-9]*\.[0-9]*\z");
      Assert.True(regex.Match(result).Success, "The format must be +{xx[x]}.{xx[x]}{xx[x]}{xx[x]}");
    }
  }
}
