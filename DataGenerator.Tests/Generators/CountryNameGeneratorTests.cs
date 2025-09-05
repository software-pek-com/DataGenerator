using DataGenerator.Generators;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class CountryNameGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      var target = new CountryNameGenerator();
      Assert.NotNull(target);

      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }
  }
}
