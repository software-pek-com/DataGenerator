using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class UsernameGeneratorTests
  {
    [Fact]
    public void Constructor_NegativeLength()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new UsernameGenerator(-1));
    }

    [Fact]
    public void Constructor_ZeroLength()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new UsernameGenerator(0));
    }

    [Fact]
    public void Constructor()
    {
      var target = new UsernameGenerator();
      Assert.NotNull(target);
      Assert.Equal(4, target.UsernameLength);
    }

    [Fact]
    public void Constructor_Length()
    {
      var target = new UsernameGenerator(13);
      Assert.NotNull(target);
      Assert.Equal(13, target.UsernameLength);
    }

    [Fact]
    public void NewValue_CorrectLength()
    {
      var target = new UsernameGenerator(13);
      Assert.Equal(target.UsernameLength, ((string)target.New()).Length);
    }

    [Fact]
    public void NewValue_Different()
    {
      var target = new UsernameGenerator();

      var result1 = target.New();
      var result2 = target.New();

      Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void NewUsername_NegativeLength()
    {
      var target = new UsernameGenerator();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.NewUsername(-1));
    }

    [Fact]
    public void NewUsername_ZeroLength()
    {
      var target = new UsernameGenerator();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.NewUsername(0));
    }

    [Fact]
    public void NewUsername_OK()
    {
      var target = new UsernameGenerator();
      var result = target.NewUsername(13);
      Assert.NotNull(result);
      Assert.Equal(13, result.Length);
    }
  }
}
