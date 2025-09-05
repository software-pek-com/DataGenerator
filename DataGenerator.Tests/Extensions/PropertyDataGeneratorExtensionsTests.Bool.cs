using DataGenerator.Generators;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsBoolTests
  {
    private const int _NumberOfIterations = 100;

    private class ObjToBuild
    {
      public bool IsNeverNull { get; set; }
      public bool? IsMaybeNull { get; set; }
    }

    [Fact]
    public void AsBool_NullPropertyValueGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, bool>)null!).AsBool(0.0));
    }

    [Fact]
    public void AsBool_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<bool>(o => o.IsNeverNull).AsBool(-0.5));
    }

    [Fact]
    public void AsBool_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<bool>(o => o.IsNeverNull).AsBool(1.1));
    }

    [Fact]
    public void AsTrue_NullPropertyValueGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, bool>)null!).AsTrue());
    }

    [Fact]
    public void AsFalse_NullPropertyValueGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, bool>)null!).AsFalse());
    }

    [Fact]
    public void AsBoolOrNull_NullPropertyValueGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, bool?>)null!).AsBoolOrNull(0.1));
    }

    [Fact]
    public void AsBoolOrNull_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<bool?>(o => o.IsNeverNull).AsBoolOrNull(-0.1));
    }

    [Fact]
    public void AsBoolOrNull_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<bool?>(o => o.IsNeverNull).AsBoolOrNull(1.1));
    }

    [Fact]
    public void AsBool()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<bool>(o => o.IsNeverNull).AsBool(0.5);
      var result = target.Generate();
      Assert.NotNull(result);
    }

    [Fact]
    public void AsTrue()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<bool>(o => o.IsNeverNull).AsTrue();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.True(result.IsNeverNull);
    }

    [Fact]
    public void AsFalse()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<bool>(o => o.IsNeverNull).AsFalse();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(result.IsNeverNull);
    }

    [Fact]
    public void AsBoolOrNull_SomeNulls()
    {
      var nullProbability = 0.9;
      var expectedNumberOfNulls = _NumberOfIterations * nullProbability;

      var target = new DataGenerator<ObjToBuild>();
      target.For<bool>(o => o.IsNeverNull).AsFalse();

      var results = target.Generate(_NumberOfIterations);
      Assert.NotNull(results);
      Assert.Equal(_NumberOfIterations, results.Count());

      var nullCount = 0;
      foreach (var result in results)
      {
        if (!result.IsMaybeNull.HasValue)
        {
          ++nullCount;
        }
      }

      Assert.True(20 > Math.Abs(expectedNumberOfNulls - nullCount));
    }
  }
}
