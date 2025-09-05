using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsDecimalTests
  {
    private class ObjToBuild
    {
      public decimal Value { get; set; }
    }

    private class NullableObjToBuild
    {
      public decimal? Value { get; set; }
    }

    [Fact]
    public void AsDecimal_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => ((PropertyDataGenerator<ObjToBuild, decimal>)null!).AsDecimal());
    }

    [Fact]
    public void AsDecimal_WithinGivenValues_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.Value).AsDecimal(0.1M, 0.2M, 0.3M);

      var result = target.Generate();

      Assert.NotNull(result);
      decimal? value = result.Value;
      Assert.NotNull(value);
      Assert.True(value == 0.1M || value == 0.2M || value == 0.3M);
    }

    [Fact]
    public void AsNullableDecimal_AlwaysNull()
    {
      var target = new DataGenerator<NullableObjToBuild>();
      target.For(o => o.Value).AsDecimalOrNull(1, 10, 20, 1.0);

      var result = target.Generate();

      Assert.NotNull(result);
      decimal? value = result.Value;
      Assert.Null(value);
    }

    [Fact]
    public void AsNullableDecimal_NeverNull()
    {
      var target = new DataGenerator<NullableObjToBuild>();
      target.For(o => o.Value).AsDecimalOrNull(1, 10, 20, 0.0);

      var result = target.Generate();

      Assert.NotNull(result);
      decimal? value = result.Value;
      Assert.NotNull(value);
      Assert.True(value >= 1);
      Assert.True(value <= 10);
    }

    [Fact]
    public void AsNullableDecimal_RespectDecimalCount()
    {
      var target = new DataGenerator<NullableObjToBuild>();
      target.For(o => o.Value).AsDecimalOrNull(1, 2, 2, 0.0);

      var result = target.Generate();

      Assert.NotNull(result);
      decimal? value = result.Value;
      Assert.NotNull(value);
      Assert.Equal(4, value.ToString()!.Length);
    }
  }
}