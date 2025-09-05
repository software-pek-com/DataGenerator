using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsIntegerTests
  {
    private class ObjToBuild
    {
      public int Value { get; set; }
      public int? NullableValue { get; set; }
    }

    [Fact]
    public void AsInteger_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, int>)null!).AsInteger());
    }

    [Fact]
    public void AsInteger_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.Value).AsInteger();
      
      var result = target.Generate();
      
      Assert.NotNull(result);
      Assert.True(int.MinValue <= result.Value);
      Assert.True(result.Value < int.MaxValue);
    }

    [Fact]
    public void AsInteger_SmallerThan_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, int>)null!).AsInteger(99));
    }

    [Fact]
    public void AsInteger_SmallerThan_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      int maxValue = 100;
      target.For(o => o.Value).AsInteger(maxValue);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(int.MinValue <= result.Value);
      Assert.True(result.Value < maxValue);
    }

    [Fact]
    public void AsInteger_InRange_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, int>)null!).AsInteger(0, 100));
    }

    [Fact]
    public void AsInteger_InRange_MaxBelowMin()
    {
      var target = new DataGenerator<ObjToBuild>();
      int maxValue = 0;
      int minValue = 99;

      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For(o => o.Value).AsInteger(minValue, maxValue));
    }

    [Fact]    
    public void AsInteger_InRange_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      int minValue = -10;
      int maxValue = 1000;
      target.For(o => o.Value).AsInteger(minValue, maxValue);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(minValue <= result.Value);
      Assert.True(result.Value < maxValue);
    }

    [Fact]
    public void AsIntegerNullable_InRange_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, int?>)null!).AsInteger(0, 100));
    }

    [Fact]
    public void AsIntegerNullable_InRange_MaxBelowMin()
    {
      var target = new DataGenerator<ObjToBuild>();
      int maxValue = 0;
      int minValue = 99;
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For(o => o.NullableValue).AsInteger(minValue, maxValue));
    }

    [Fact]
    public void AsIntegerNullable_InRange_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      int minValue = -10;
      int maxValue = 1000;
      target.For(o => o.NullableValue).AsInteger(minValue, maxValue);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.NotNull(result.NullableValue);
      Assert.True(minValue <= result.NullableValue);
      Assert.True(result.NullableValue < maxValue);
    }
  }
}
