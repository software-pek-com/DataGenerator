using DataGenerator.Generators;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class RandomEmailAddressGeneratorTests
  {
    private const bool _IsFemale = true;
    private const bool _IsMale = false;
    private static readonly Regex _EmailValidationRegex =
        new Regex("^(?<user>[^@]+)@(?<host>.+)$", RegexOptions.Compiled);

    /// <summary>
    /// Represents a simple email address format validator.
    /// </summary>
    private static bool IsValidEmailFormat(string emailAddress)
    {
      var match = _EmailValidationRegex.Match(emailAddress);
      return match.Success;
    }

    private class RandomEmailAddressGeneratorMock : RandomEmailAddressGenerator
    {
      public new FormatDescriptor Descriptor { get { return base.Descriptor; } }

      public IDictionary<int, object> ParameterValueMap { get { return _ParameterValueMap; } }
    }

    [Fact]
    public void Constructor()
    {
      var target = new RandomEmailAddressGeneratorMock();

      Assert.NotNull(target);
      Assert.NotNull(target.Descriptor);
      Assert.Equal("{0}.{1}@{2}", target.Descriptor.Pattern);
      Assert.Equal(3, target.ParameterValueMap.Count);
      Assert.True(target.ParameterValueMap[0] is FirstNameGenerator);
      Assert.True(target.ParameterValueMap[1] is LastNameGenerator);
      Assert.True(target.ParameterValueMap[2] is ResourceFileStringGenerator);
      Assert.NotEmpty(((ResourceFileStringGenerator)target.ParameterValueMap[2]).ValueDataSource.GetAllValues());
    }

    [Fact]
    public void NewValue_RandomNames()
    {
      var target = new RandomEmailAddressGenerator();

      var result = target.New();

      Assert.NotNull(result);
      Assert.True(IsValidEmailFormat(result));
    }
  }
}
