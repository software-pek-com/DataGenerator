using DataGenerator.Generators;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsGuidTests
  {
    const int _NumberOfIterations = 100;

    private class ObjToBuild
    {
      public Guid NeverNull { get; set; }
      public Guid? MaybeNull { get; set; }
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuid_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, Guid>)null!).AsGuid());
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuidOrNull_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, Guid?>)null!).AsGuidOrNull(0.5));
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuidOrNull_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<Guid?>(o => o.MaybeNull).AsGuidOrNull(-0.1));
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuidOrNull_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<Guid?>(o => o.MaybeNull).AsGuidOrNull(1.1));
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuid_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<Guid>(o => o.NeverNull).AsGuid();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.NotEqual(Guid.Empty, result.NeverNull);
    }

    [Fact]
    public void PropertyDataGeneratorExtensions_AsGuidOrNull_SomeNulls()
    {
      var nullProbability = 0.9;
      var expectedNumberOfNulls = _NumberOfIterations * nullProbability;

      var target = new DataGenerator<ObjToBuild>();
      target.For<Guid?>(o => o.MaybeNull).AsGuidOrNull(nullProbability);

      var results = target.Generate(_NumberOfIterations);
      Assert.NotNull(results);
      Assert.Equal(_NumberOfIterations, results.Count());

      var nullCount = 0;
      foreach (var result in results)
      {
        if (!result.MaybeNull.HasValue)
        {
          ++nullCount;
        }
      }

      Assert.True(20 > Math.Abs(expectedNumberOfNulls - nullCount), $"Got {nullCount} nulls");
    }
  }
}
