using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class DecimalGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      var target = new DecimalGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void New()
    {
      var target = new DecimalGenerator();

      var result = target.New();

      Assert.IsType<decimal>(result);
    }

    [Fact]
    public void SmallerThan_MaxTooLow()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentOutOfRangeException>(() => target.SmallerThan(-0.1m));
    }

    [Fact]
    public void SmallerThan()
    {
      var target = new DecimalGenerator();

      const decimal expectedMax = 3.14m;
      var result = target.SmallerThan(expectedMax);

      Assert.True(result >= 0m);
      Assert.True(result < expectedMax);
    }

    [Fact]
    public void InRange_MinEqualsMax()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentException>(() => target.InRange(3.14m, 3.14m));
    }

    [Fact]
    public void InRange_MinBiggerThanMax()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentException>(() => target.InRange(3.15m, 3.14m));
    }

    [Fact]
    public void InRange()
    {
      var target = new DecimalGenerator();

      const decimal expectedMin = 1m;
      const decimal expectedMax = 3.14m;

      var result = target.InRange(expectedMin, expectedMax);

      Assert.True(result >= expectedMin);
      Assert.True(result < expectedMax);
    }

    [Fact]
    public void InRangeWithDecimals_MinEqualsMax()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentException>(() => target.InRangeWithDecimals(3.14m, 3.14m, 1));
    }

    [Fact]
    public void InRangeWithDecimals_MinBiggerThanMax()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentException>(() => target.InRangeWithDecimals(3.15m, 3.14m, 1));
    }

    [Fact]
    public void InRangeWithDecimals_Ok()
    {
      var target = new DecimalGenerator();

      const decimal expectedMin = 1m;
      const decimal expectedMax = 3.14m;

      var result = target.InRangeWithDecimals(expectedMin, expectedMax, 1);

      Assert.True(result >= expectedMin);
      Assert.True(result < expectedMax);
    }

    [Fact]
    public void InRangeWithDecimals_DecimalsToSmall()
    {
      var target = new DecimalGenerator();

      Assert.Throws<ArgumentException>(() => target.InRangeWithDecimals(3.14m, 3.14m, -1));
    }
    
    /// <summary>
    /// This test does not cover the <see cref="RandomExtensions.NextDecimal(System.Random)"/> with precision and scale. See the appropriate tests for the Extension.
    /// </summary>
    [Fact]
    public void OfPrecisionAndScale()
    {
      var target = new DecimalGenerator();

      Assert.IsType<decimal>(target.OfPrecisionAndScale(1,1));
    }

    [Fact]
    public void WithinGivenValues_NoValues()
    {
      var generator = new DecimalGenerator();
      Assert.Throws<ArgumentException>(() => generator.OneOf());            
    }

    [Fact]
    public void WithinGivenValues()
    {
      var generator = new DecimalGenerator();
      var result = generator.OneOf(0.1M, 0.2M, 0.3M);

      Assert.True(result == 0.1M || result == 0.2M || result == 0.3M);
    }
  }
}