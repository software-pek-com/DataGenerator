using DataGenerator.Generators;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class CharacterGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      new CharacterGenerator();
    }

    [Fact]
    public void New()
    {
      var target = new CharacterGenerator();

      Assert.NotNull(target.New());
    }

    [Fact]
    public void New_IsCapitalChar()
    {
      var target = new CharacterGenerator();
      var result = target.New();

      Assert.True(result is char);
      var character = (char)result;

      Assert.True((int)character >= (int)'A');
      Assert.True((int)character <= (int)'Z');
    }
  }
}
