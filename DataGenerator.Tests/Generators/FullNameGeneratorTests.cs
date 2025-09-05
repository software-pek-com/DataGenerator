using DataGenerator.Generators;
using System.Collections.Generic;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class FullNameGeneratorTests
  {
    private class FullNameGeneratorMock : FullNameGenerator
    {
      public FullNameGeneratorMock() : base() { }

      public new FormatDescriptor Descriptor { get { return base.Descriptor; } }

      public IDictionary<int, object> ParameterValueMap { get { return _ParameterValueMap; } }
    }

    [Fact]
    public void Constructor()
    {
      var target = new FullNameGeneratorMock();
      Assert.NotNull(target);

      Assert.NotNull(target.Descriptor);
      Assert.Equal("{0} {1}", target.Descriptor.Pattern);
      Assert.NotNull(target.ParameterValueMap);
      Assert.True(target.ParameterValueMap.Count > 0);
      Assert.True(target.ParameterValueMap.Values.Count > 0);
    }

    [Fact]
    public void NewValue()
    {
      var target = new FullNameGeneratorMock();

      var result = target.New();

      Assert.NotNull(result);
      Assert.True(result.Contains(' '));
    }
  }
}
