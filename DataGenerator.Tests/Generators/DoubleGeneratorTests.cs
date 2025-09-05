using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class DoubleGeneratorTests
  {
    private const int NumberOfIterations = 1000000;

    [Fact]
    public void MaxLessThanMin()
    {
      var target = new DoubleGenerator();

      Assert.Throws<ArgumentException>(() => target.InRange(2.0, 1.0));
    }

    [Fact]
    public void MaxIsZero()
    {
      var target = new DoubleGenerator();

      Assert.Throws<ArgumentOutOfRangeException>(() => target.SmallerThan(0.0));
    }

    [Fact]
    public void Constructor()
    {
      var target = new DoubleGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void NewValue()
    {
      var target = new DoubleGenerator();

      for (int i = 0; i < NumberOfIterations; ++i)
      {
        var newValue = (double)target.New();

        Assert.True(newValue >= 0.0);
        Assert.True(newValue < 1.0);
      }
    }

    [Fact]
    public void NewValue_InASpecificRange()
    {
      var target = new DoubleGenerator();
      var expectedMin = -11.0;
      var expectedMax = 23.0;

      for (int i = 0; i < NumberOfIterations; ++i)
      {
        var newValue = (double)target.InRange(expectedMin, expectedMax);

        Assert.True(newValue >= expectedMin);
        Assert.True(newValue < expectedMax);
      }
    }

    [Fact]
    public void NewValue_PositiveUpToMax()
    {
      var target = new DoubleGenerator();
      var expectedMin = 0.0;
      var expectedMax = 23.0;

      for (int i = 0; i < NumberOfIterations; ++i)
      {
        var newValue = (double)target.SmallerThan(expectedMax);

        Assert.True(newValue >= expectedMin);
        Assert.True(newValue < expectedMax);
      }
    }
  }
}
