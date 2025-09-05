using DataGenerator.Generators;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class PostalAddressGeneratorTests
  {
    private class PostalAddressGeneratorMock : PostalAddressGenerator
    {
      public new FormatDescriptor Descriptor { get { return base.Descriptor; } }

      public IDictionary<int, object> ParameterValueMap { get { return _ParameterValueMap; } }
    }

    [Fact]
    public void Constructor()
    {
      var target = new PostalAddressGeneratorMock();

      Assert.NotNull(target);

      Assert.NotNull(target.Descriptor);
      Assert.Equal("{0} {1}, {2}, {3} {4}", target.Descriptor.Pattern);
      Assert.NotNull(target.ParameterValueMap);
      Assert.True(target.ParameterValueMap.Count > 0);
      Assert.True(target.ParameterValueMap.Values.Count > 0);
    }

    [Fact]
    public void NewValue()
    {
      var target = new PostalAddressGeneratorMock();
      var result = target.New();

      Assert.False(string.IsNullOrEmpty(result));
      Assert.Contains(",", result);
    }

    [Fact]
    public void NewAddress()
    {
      var target = new PostalAddressGeneratorMock();

      var expectedCountry = "Belgium";

      var result = target.NewAddress(expectedCountry);
      Assert.NotNull(result);
      Assert.True(result is ExpandoObject);

      var resultMap = result as IDictionary<string, object>;

      Assert.True(resultMap!.ContainsKey("HouseNumber"));
      Assert.True(resultMap!.ContainsKey("StreetName"));
      Assert.True(resultMap!.ContainsKey("Postcode"));
      Assert.True(resultMap!.ContainsKey("City"));
      Assert.True(resultMap!.ContainsKey("Country"));
    }
  }
}
