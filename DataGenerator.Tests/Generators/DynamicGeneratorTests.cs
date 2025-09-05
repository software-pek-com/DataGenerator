using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class DynamicGeneratorTests
  {
    [Fact]
    public void Constructor_NullFactoryMethod()
    {
      Assert.Throws<ArgumentNullException>(() => new DynamicValueGenerator<int>(null!));
    }

    [Fact]
    public void Constructor()
    {
      Func<int> intFactory = () => 1;

      var target = new DynamicValueGenerator<int>(intFactory);
      Assert.NotNull(target);
      Assert.Same(intFactory, target._FactoryMethod);
    }

    [Fact]
    public void NewValue()
    {
      var expectedResult = 123;
      Func<int> intFactory = () => expectedResult;

      var target = new DynamicValueGenerator<int>(intFactory);

      var result = target.New();
      Assert.Equal(expectedResult, result);
    }
  }
}
