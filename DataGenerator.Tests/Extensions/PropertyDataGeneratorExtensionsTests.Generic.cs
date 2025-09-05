using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsGenericTests
  {
    private class ObjToBuild
    {
      public int Value { get; set; }
    }

    [Fact]
    public void AsNew_NullPropertyValueGenerator()
    {
      Func<int> intFactory = () => 123;
      Assert.Throws<ArgumentNullException>(() =>
          ((PropertyDataGenerator<ObjToBuild, int>) null!).AsNew(intFactory));
    }

    [Fact]
    public void AsNew_NullFactoryMethod()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentNullException>(() =>
        target.For<int>(o => o.Value).AsNew(null!));
    }
  }
}
