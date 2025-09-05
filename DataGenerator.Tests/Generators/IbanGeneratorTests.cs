using DataGenerator.Generators;
using System.Text.RegularExpressions;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class IbanGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      new IbanGenerator();
    }

    [Fact]
    public void New_Ok()
    {
      var target = new IbanGenerator();

      var result = target.New();

      Assert.NotNull(result);

      var regex = new Regex(@"^[A-Z]{2}[0-9]{2}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}\z");
      Assert.True(regex.Match(result).Success);
    }
  }
}
