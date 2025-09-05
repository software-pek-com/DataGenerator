using DataGenerator.Generators;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsDateTimeTests
  {
    private const int _NumberOfIterations = 100;
    private const int _AllowedDeviations = (int)(_NumberOfIterations * 0.1);

    private class ObjToBuild
    {
      public DateTime NeverNull { get; set; }
      public DateTime? MaybeNull { get; set; }
    }

    [Fact]
    public void AsBirthDate_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, DateTime>)null!).AsBirthDate());
    }

    [Fact]
    public void AsBirthDateOrNull_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, DateTime?>)null!).AsBirthDateOrNull(0.5));
    }

    [Fact]
    public void AsBirthDateOrNull_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<DateTime?>(o => o.NeverNull).AsBirthDateOrNull(-0.1));
    }

    [Fact]
    public void AsBirthDateOrNull_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<DateTime?>(o => o.NeverNull).AsBirthDateOrNull(1.1));
    }

    [Fact]
    public void AsDateTime_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, DateTime>)null!).AsDateTime());
    }

    [Fact]
    public void AsDateTimeOrNull_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, DateTime?>)null!).AsDateTimeOrNull(0.5));
    }

    [Fact]
    public void AsDateTimeOrNull_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<DateTime?>(o => o.NeverNull).AsDateTimeOrNull(-0.1));
    }

    [Fact]
    public void AsDateTimeOrNull_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For<DateTime?>(o => o.NeverNull).AsDateTimeOrNull(1.1));
    }

    [Fact]
    public void AsBirthDate_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<DateTime>(o => o.NeverNull).AsBirthDate();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.NotEqual(DateTime.MinValue, result.NeverNull);
      Assert.NotEqual(DateTime.MaxValue, result.NeverNull);
    }

    [Fact]
    public void AsDateTime_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<DateTime>(o => o.NeverNull).AsDateTime();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.NotEqual(DateTime.MinValue, result.NeverNull);
      Assert.NotEqual(DateTime.MaxValue, result.NeverNull);
    }

    /// <summary>
    /// This test fails once in a while.
    /// </summary>
    //[Fact]
    public void AsBirthDateOrNull_SomeNulls()
    {
      var nullProbability = 0.9;
      var expectedNumberOfNulls = _NumberOfIterations * nullProbability;

      var target = new DataGenerator<ObjToBuild>();
      target.For<DateTime?>(o => o.MaybeNull).AsBirthDateOrNull(nullProbability);

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

      Assert.True(_AllowedDeviations > Math.Abs(expectedNumberOfNulls - nullCount));
    }

    /// <summary>
    /// This test fails once in a while.
    /// </summary>
    //[Fact]
    public void AsDateTimeOrNull_SomeNulls()
    {
      var nullProbability = 0.9;
      var expectedNumberOfNulls = _NumberOfIterations * nullProbability;

      var target = new DataGenerator<ObjToBuild>();
      target.For<DateTime?>(o => o.MaybeNull).AsDateTimeOrNull(nullProbability);

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

      Assert.True(_AllowedDeviations > Math.Abs(expectedNumberOfNulls - nullCount));
    }
  }
}
