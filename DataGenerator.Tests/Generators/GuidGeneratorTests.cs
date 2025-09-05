using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class GuidGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      var target = new GuidGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void NewValue()
    {
      var target = new GuidGenerator();
      Assert.Same(typeof(Guid), target.New().GetType());
    }
  }
}
