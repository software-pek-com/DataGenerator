using DataGenerator.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  /// <summary>
  /// Unit tests for the class <see cref="IComplexGeneratorExtensionsTests"/>.
  /// </summary>
  public class IComplexGeneratorExtensionsTests
  {
    #region Helpers

    private class Item
    {
      public int Id { get; set; }
    }

    private class ItemEqualityComparer : IEqualityComparer<Item>
    {
      public bool Equals(Item? x, Item? y)
      {
          if (object.Equals(x, y)) { return true; }
          if ((x is null) || (y is null)) { return false; }
          return x.Id == y.Id;
      }

      public int GetHashCode(Item? obj)
      {
          if (obj is null) { return 0; }
          return obj.Id;
      }
    }

    #endregion

    #region GenerateDistinctInRange

    [Fact]
    public void GenerateDistinctInRange_WithNullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        IComplexGeneratorExtensions.GenerateDistinctInRange(null!, new ItemEqualityComparer(), 4));
    }

    [Fact]
    public void GenerateDistinctInRange_WithNullComparer()
    {
      var generatorMock = new ComplexGeneratorMock<Item>();

      Assert.Throws<ArgumentNullException>(() => generatorMock.GenerateDistinctInRange(null!, 4));
    }

    [Fact]
    public void GenerateDistinctInRange_WithZeroMaxNumberOfItems()
    {
      var generatorMock = new ComplexGeneratorMock<Item>();

      Assert.Throws<ArgumentOutOfRangeException>(() =>
        generatorMock.GenerateDistinctInRange(new ItemEqualityComparer(), 0));
    }

    [Fact]
    public void GenerateDistinctInRange_WithMaxOneItem_Ok()
    {
      var generatorMock = new ComplexGeneratorMock<Item>();
      var item = new Item {Id = 1};
      generatorMock.GenerateReturn = new [] { item };

      IEnumerable<Item> result = generatorMock.GenerateDistinctInRange(new ItemEqualityComparer(), 1);
      Assert.NotNull(result);
      Assert.Single(result);
      Assert.Same(item, result.Single());
      Assert.True(result.Count() <= 2);
    }

    [Fact]
    public void GenerateDistinctInRange_Ok()
    {
      var generatorMock = new ComplexGeneratorMock<Item>();
      generatorMock.GenerateReturn = new []
      {
          new Item {Id = 1},
          new Item {Id = 2},
          new Item {Id = 1},
          new Item {Id = 2}
      };

      IEnumerable<Item> result = generatorMock.GenerateDistinctInRange(new ItemEqualityComparer(), 4);
      Assert.NotNull(result);
      Assert.NotEmpty(result!);
      Assert.True(result.Count() <= 2);
    }

    #endregion
  }
}
