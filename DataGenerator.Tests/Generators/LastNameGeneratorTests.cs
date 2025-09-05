using DataGenerator.Generators;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class LastNameGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      var target = new LastNameGenerator();
      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }
  }
}
