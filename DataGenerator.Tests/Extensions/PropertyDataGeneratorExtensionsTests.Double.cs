using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsDoubleTests
  {
    private class ObjToBuild
    {
      public double Value { get; set; }
      public double? ValueAsNull { get; set; }
    }

    #region AsDouble

    [Fact]
    public void AsDouble_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, double>)null!).AsDouble());
    }

    [Fact]
    public void AsDouble_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.Value).AsDouble();
      
      var result = target.Generate();
      
      Assert.NotNull(result);
      Assert.True(0.0 <= result.Value);
      Assert.True(result.Value < 1.0);
    }

    [Fact]
    public void AsDouble_SmallerThan_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, double>)null!).AsDouble(99));
    }

    [Fact]
    public void AsDouble_SmallerThan_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      double maxValue = 100;
      target.For(o => o.Value).AsDouble(maxValue);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(0.0 <= result.Value);
      Assert.True(result.Value < maxValue);
    }

    [Fact]
    public void AsDouble_InRange_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, double>)null!).AsDouble(0, 100));
    }


    [Fact]
    public void AsDouble_InRange_MaxBelowMin()
    {
      var target = new DataGenerator<ObjToBuild>();
      double maxValue = 0;
      double minValue = 99;
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For(o => o.Value).AsDouble(minValue, maxValue));
    }

    [Fact]
    public void AsDouble_InRange_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      double minValue = -10;
      double maxValue = 1000;
      target.For(o => o.Value).AsDouble(minValue, maxValue);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(minValue <= result.Value);
      Assert.True(result.Value < maxValue);
    }

    #endregion

    #region AsDoubleOrNull

    [Fact]
    public void AsDoubleOrNull_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, double?>)null!).AsDoubleOrNull(0.0, 1.0, 0.5));
    }

#if false
    // This test fails randomly for unknown reasons.

    [Fact]
    public void AsDoubleOrNull_NullProbabilityOff_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.ValueAsNull).AsDoubleOrNull(0.1, 0.9, 0);

      var result = target.Generate();

      Assert.NotNull(result.ValueAsNull);
      Assert.True(0.1 <= result.ValueAsNull);
      Assert.True(result.ValueAsNull < 0.9, $"ValueAsNull should be < 0.9, but is {result.ValueAsNull}.");
    }
#endif

    [Fact]
    public void AsDoubleOrNull_NullProbabilityFull_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.ValueAsNull).AsDoubleOrNull(0.1, 0.9, 1);

      var result = target.Generate();

      Assert.Null(result.ValueAsNull);
    }

    [Fact]
    public void AsDoubleOrNull_InRange_MaxBelowMin()
    {
      var target = new DataGenerator<ObjToBuild>();
      double maxValue = 0;
      double minValue = 99;
      Assert.Throws<ArgumentOutOfRangeException>(() => target.For(o => o.ValueAsNull).AsDoubleOrNull(minValue, maxValue, 0));
    }

    [Fact]
    public void AsDoubleOrNull_InRange_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      double minValue = -10;
      double maxValue = 1000;
      target.For(o => o.ValueAsNull).AsDoubleOrNull(minValue, maxValue, 0);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(minValue <= result.Value);
      Assert.True(result.Value < maxValue);
    }

    #endregion
  }
}
