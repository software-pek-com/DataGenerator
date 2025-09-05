using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class IntegerGeneratorTests
  {
    private const int _NumberOfIterations = 1000000;

    [Fact]
    public void MaxLessThanMin()
    {
      var target = new IntegerGenerator();

      Assert.Throws<ArgumentException>(() => target.InRange(2, 1));
    }

    [Fact]
    public void MaxIsZero()
    {
      var target = new IntegerGenerator();

      Assert.Throws<ArgumentOutOfRangeException>(() => target.SmallerThan(0));
    }

    [Fact]
    public void Constructor()
    {
      var target = new IntegerGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void Constructor2_MinMaxEqual()
    {
      Assert.Throws<ArgumentException>(() => new IntegerGenerator(1, 1));
    }

    [Fact]
    public void Constructor2_MaxLessThanMin()
    {
      Assert.Throws<ArgumentException>(() => new IntegerGenerator(2, 1));
    }

    [Fact]
    public void Constructor2_MaxIsZero()
    {
      Assert.Throws<ArgumentException>(() => new IntegerGenerator(1, 0));
    }

    [Fact]
    public void NewValue_AllIntegers()
    {
      var target = new IntegerGenerator();

      for (int i = 0; i < _NumberOfIterations; ++i)
      {
        var newValue = target.New();

        Assert.True(newValue >= int.MinValue);
        Assert.True(newValue < int.MaxValue);
      }
    }

    [Fact]
    public void InRange_InASpecificRange()
    {
      var target = new IntegerGenerator();
      var expectedMin = -11;
      var expectedMax = 23;

      for (int i = 0; i < _NumberOfIterations; ++i)
      {
        var newValue = target.InRange(expectedMin, expectedMax);

        Assert.True(newValue >= expectedMin);
        Assert.True(newValue < expectedMax);
      }
    }

    [Fact]
    public void InRange_MinMaxEqual()
    {
      var target = new IntegerGenerator();
      Assert.Throws<ArgumentException>(() => target.InRange(2, 2));
    }

    [Fact]
    public void InRange_PositiveUpToMax()
    {
      var target = new IntegerGenerator();
      var expectedMin = 0;
      var expectedMax = 23;

      for (int i = 0; i < _NumberOfIterations; ++i)
      {
        var newValue = (int)target.SmallerThan(expectedMax);

        Assert.True(newValue >= expectedMin);
        Assert.True(newValue < expectedMax);
      }
    }
  }
}
