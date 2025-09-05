using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class BooleanGeneratorTests
  {
    [Fact]
    public void Constructor1()
    {
      var target = new BooleanGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void NewValue()
    {
      var target = new BooleanGenerator();
      var result = target.New();
    }

    [Fact]
    public void NewBoolean_NegativeProbability()
    {
      var target = new BooleanGenerator();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.NewBoolean(-0.5));
    }

    [Fact]
    public void NewBoolean_ProbabilityTooBig()
    {
      var target = new BooleanGenerator();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.NewBoolean(1.3));
    }

    [Fact]
    public void AsTrue()
    {
      var target = new BooleanGenerator();
      Assert.True(target.AsTrue());
    }

    [Fact]
    public void AsFalse()
    {
      var target = new BooleanGenerator();
      Assert.False(target.AsFalse());
    }
  }
}
