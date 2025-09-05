using DataGenerator.Generators;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsEnumTests
  {
    #region Helpers

    private enum EnumWithValues { A, B, C }

    private class ObjToBuild
    {
      public EnumWithValues EnumValue { get; set; }

      public EnumWithValues? NullableEnumValue { get; set; }
    }

    #endregion

    #region AsEnum

    [Fact]
    public void AsEnum_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, EnumWithValues>)null!).AsEnum<ObjToBuild, EnumWithValues>());
    }

    [Fact]
    public void AsEnum_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.EnumValue).AsEnum();

      var result = target.Generate();

      Assert.NotNull(result);
      EnumWithValues enumValue = result.EnumValue;
      Assert.True(
          enumValue == EnumWithValues.A ||
          enumValue == EnumWithValues.B ||
          enumValue == EnumWithValues.C);
    }

    #endregion

    #region AsEnumExcept

    [Fact]
    public void AsEnumExcept_NullBlacklist()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, EnumWithValues>)null!).AsEnumExcept<ObjToBuild, EnumWithValues>(null!));
    }

    [Fact]
    public void AsEnumExcept_EmptyBlacklist()
    {
      var target = new DataGenerator<ObjToBuild>();

      Assert.Throws<ArgumentException>(() =>
        target.For(o => o.EnumValue).AsEnumExcept(Enumerable.Empty<EnumWithValues>()));
    }

    [Fact]
    public void AsEnumExcept_Ok()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.EnumValue).AsEnumExcept(new[] { EnumWithValues.A, EnumWithValues.B });

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.Equal(EnumWithValues.C, result.EnumValue);
    }

    #endregion

    #region AsEnumOrNull

    [Fact]
    public void AsEnumOrNull_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => 
        ((PropertyDataGenerator<ObjToBuild, EnumWithValues?>)null!).AsEnumOrNull(0.50));
    }

    [Fact]
    public void AsEnumOrNull_ProbabilityOfNullTooLow()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For(o => o.NullableEnumValue).AsEnumOrNull(-0.1));
    }

    [Fact]
    public void AsEnumOrNull_ProbabilityOfNullTooHigh()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For(o => o.NullableEnumValue).AsEnumOrNull(1.1));
    }

    [Fact]
    public void AsEnumOrNull_ProbabilityOfNullIsZero()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For(o => o.NullableEnumValue).AsEnumOrNull(0.0);

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.True(result.NullableEnumValue.HasValue);
      EnumWithValues enumValue = result.NullableEnumValue!.Value;
      Assert.True(
          enumValue == EnumWithValues.A ||
          enumValue == EnumWithValues.B ||
          enumValue == EnumWithValues.C);
    }

    #endregion
  }
}
