using DataGenerator.Generators;
using System.Collections.Generic;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class StreetNameGeneratorTests
  {
    private class Accessor : StreetNameGenerator
    {
      public new FormatDescriptor Descriptor { get { return base.Descriptor; } }

      public IDictionary<int, object> ParameterValueMap { get { return _ParameterValueMap; } }
    }

    [Fact]
    public void Constructor()
    {
      var target = new Accessor();
      Assert.NotNull(target);

      Assert.NotNull(target.Descriptor);
      Assert.Equal("{0} {1}", target.Descriptor.Pattern);

      Assert.NotNull(target.ParameterValueMap);
      Assert.True(target.ParameterValueMap.Count > 0);
      
      Assert.NotNull(target.ParameterValueMap[0]);
      Assert.Equal(typeof(ResourceFileStringGenerator), target.ParameterValueMap[0].GetType());
      Assert.NotEmpty(((ResourceFileStringGenerator)target.ParameterValueMap[0]).ValueDataSource.GetAllValues());
      Assert.Equal(typeof(List<string>), target.ParameterValueMap[1].GetType());
      Assert.True(((List<string>)target.ParameterValueMap[1]).Count > 0);
    }

    [Fact]
    public void NewValue()
    {
      var target = new Accessor();
      var result = target.New();
      Assert.NotNull(result);
      Assert.True(result.Contains(' '));
    }
  }
}
