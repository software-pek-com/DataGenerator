using DataGenerator.Generators;
using System.Text.RegularExpressions;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class WebsiteGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      new WebsiteGenerator();
    }

    [Fact]
    public void New_Ok()
    {
      var target = new WebsiteGenerator();

      string result = target.New();

      Assert.NotNull(result);

      var regex = new Regex(@"^www\.[A-Z]+.[a-z]+\z");
      Assert.True(regex.Match(result).Success);
    }
  }
}
