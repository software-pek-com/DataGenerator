using DataGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class EnumGeneratorTests
  {
    private enum EnumWithoutValues { }

    private enum EnumWithValues { A, B, C }

    [Fact]
    public void Constructor()
    {
      var target = new EnumGenerator<EnumWithValues>();
      Assert.NotNull(target);
    }

    [Fact]
    public void Constructor_EnumWithNoValues()
    {
      Assert.Throws<TypeInitializationException>(() => new EnumGenerator<EnumWithoutValues>());
    }

    [Fact]
    public void Constructor_WithNullBlacklist()
    {
      Assert.Throws<ArgumentNullException>(() => new EnumGenerator<EnumWithValues>(null!));
    }

    [Fact]
    public void Constructor_EmptyBlacklist()
    {
      Assert.Throws<ArgumentException>(() => new EnumGenerator<EnumWithValues>(Enumerable.Empty<EnumWithValues>()));
    }

    [Fact]
    public void NewValue()
    {
      var target = new EnumGenerator<EnumWithValues>();
      
      var result = target.New();

      Assert.True(result == EnumWithValues.A || result == EnumWithValues.B || result == EnumWithValues.C);
    }

    #region NewValueFromWhiteList

    [Fact]
    public void NewValueFromWhiteList_WithNullList()
    {
      var target = new EnumGenerator<EnumWithValues>();

      Assert.Throws<ArgumentNullException>(() => target.NewValueFromWhiteList(null!));
    }

    [Fact]
    public void NewValueFromWhiteList_WithEmptyList()
    {
      var target = new EnumGenerator<EnumWithValues>();

      Assert.Throws<ArgumentException>(() => target.NewValueFromWhiteList(new EnumWithValues[0]));
    }

    [Fact]
    public void NewValueFromWhiteList_WithListOfOneItem_Ok()
    {
      var target = new EnumGenerator<EnumWithValues>();

      var result = target.NewValueFromWhiteList(new[] { EnumWithValues.C });

      Assert.Equal(EnumWithValues.C, result);
    }

    [Fact]
    public void NewValueFromWhiteList_Ok()
    {
      var target = new EnumGenerator<EnumWithValues>();

      var whiteList = new HashSet<EnumWithValues>() { EnumWithValues.A, EnumWithValues.B };

      var result = target.NewValueFromWhiteList(whiteList);

      Assert.Contains(result, whiteList);
    }

    [Fact]
    public void NewValueFromWhiteList_WithAllEnumValuesInTheList_Ok()
    {
      var target = new EnumGenerator<EnumWithValues>();

      var whiteList = new HashSet<EnumWithValues>() { EnumWithValues.A, EnumWithValues.B, EnumWithValues.C };

      var result = target.NewValueFromWhiteList(whiteList);

      Assert.Contains(result, whiteList);
    }

    #endregion

    #region NewValueNotFromBlackList

    [Fact]
    public void NewValueNotFromBlackList_WithNullList()
    {
      var target = new EnumGenerator<EnumWithValues>();

      Assert.Throws<ArgumentNullException>(() => target.NewValueNotFromBlackList(null!));
    }

    [Fact]
    public void NewValueNotFromBlackList_WithEmptyList()
    {
      var target = new EnumGenerator<EnumWithValues>();

      Assert.Throws<ArgumentException>(() => target.NewValueNotFromBlackList(new EnumWithValues[0]));
    }

    [Fact]
    public void NewValueNotFromBlackList_Ok()
    {
      var target = new EnumGenerator<EnumWithValues>();

      var result = target.NewValueNotFromBlackList(new[] { EnumWithValues.A, EnumWithValues.C });

      Assert.Equal(EnumWithValues.B, result);
    }

    [Fact]
    public void NewValueNotFromBlackList_WithTwiceSameEnumValueInTheList_Ok()
    {
      var target = new EnumGenerator<EnumWithValues>();

      var result = target.NewValueNotFromBlackList(new[] { EnumWithValues.A, EnumWithValues.A, EnumWithValues.C });

      Assert.Equal(EnumWithValues.B, result);
    }

    [Fact]
    public void NewValueNotFromBlackList_WithAllEnumValuesInTheList()
    {
      var target = new EnumGenerator<EnumWithValues>();

      Assert.Throws<InvalidOperationException>(() =>
        target.NewValueNotFromBlackList(new[] { EnumWithValues.A, EnumWithValues.B, EnumWithValues.C }));
    }

    #endregion
  }
}
