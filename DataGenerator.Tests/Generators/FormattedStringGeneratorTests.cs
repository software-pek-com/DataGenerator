using DataGenerator.Core;
using DataGenerator.Generators;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class FormattedStringGeneratorTests
  {
    private static readonly List<string> _FirstNames = new List<string> { "Bart", "Homer", "Lisa", "Marge" };
    private static readonly List<string> _LastNames = new List<string> { "Simpson", "Burns" };

    private class DataGeneratorMock : IValueGenerator
    {
      public object New()
      {
        throw new NotImplementedException();
      }
    }

    private class FormattedStringGeneratorMock : FormattedStringGenerator
    {
      public FormattedStringGeneratorMock(string pattern)
          : base(pattern)
      {
      }

      public FormattedStringGeneratorMock(FormatDescriptor descriptor)
          : base(descriptor)
      {
      }

      public new void RegisterValues(int parameter, IValueGenerator values)
      {
        base.RegisterValues(parameter, values);
      }

      public new void RegisterValues(int parameter, IList<string> values)
      {
        base.RegisterValues(parameter, values);
      }

      public new FormatDescriptor Descriptor { get { return base.Descriptor; } }

      public new IDictionary<int, object> ParameterValueMap { get { return base._ParameterValueMap; } }
    }

    [Fact]
    public void Constructor1_NullString()
    {
      Assert.Throws<ArgumentNullException>(() => new FormattedStringGenerator((string)null!));
    }

    [Fact]
    public void Constructor2_NullDescriptor()
    {
      Assert.Throws<ArgumentNullException>(() => new FormattedStringGenerator((FormatDescriptor)null!));
    }

    [Fact]
    public void Constructor1_OK()
    {
      var expectedPattern = "{0} a {1} b";
      var target = new FormattedStringGeneratorMock(expectedPattern);

      Assert.NotNull(target);
      Assert.NotNull(target.Descriptor);
      Assert.Equal(expectedPattern, target.Descriptor.Pattern);
    }

    [Fact]
    public void Constructor2_OK()
    {
      var expectedDescriptor = new FormatDescriptor("{0} a {1} b");
      var target = new FormattedStringGeneratorMock(expectedDescriptor);

      Assert.NotNull(target);
      Assert.Same(expectedDescriptor, target.Descriptor);
    }

    [Fact]
    public void RegisterValues_NegativeParameter()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<ArgumentOutOfRangeException>(() => target.RegisterValues(-1, _FirstNames));
    }

    [Fact]
    public void RegisterValues_TooBigParameter()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<ArgumentOutOfRangeException>(() => target.RegisterValues(2, _FirstNames));
    }

    [Fact]
    public void RegisterValues1_NullValues()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<ArgumentNullException>(() => target.RegisterValues(0, (IValueGenerator)null!));
    }

    [Fact]
    public void RegisterValues2_NullValues()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<ArgumentNullException>(() => target.RegisterValues(0, (IList<string>)null!));
    }

    [Fact]
    public void RegisterValues2_EmptyValues()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<ArgumentException>(() => target.RegisterValues(0, new List<string> { }));
    }

    [Fact]
    public void RegisterValues2_List()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      target.RegisterValues(0, _FirstNames);

      Assert.Same(_FirstNames, target.ParameterValueMap[0]);
    }

    [Fact]
    public void RegisterValues_DataGenerator()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      var expectedValues = new DataGeneratorMock();
      target.RegisterValues(0, expectedValues);

      Assert.Same(expectedValues, target.ParameterValueMap[0]);
    }

    [Fact]
    public void NewValue_NoRegistration()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      Assert.Throws<InvalidOperationException>(() => target.New());
    }

    [Fact]
    public void NewValue_MissingRegistration()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      target.RegisterValues(1, _LastNames);

      Assert.Throws<InvalidOperationException>(() => target.New());
    }

    [Fact]
    public void NewValue()
    {
      var target = new FormattedStringGeneratorMock("{0} {1}");

      target.RegisterValues(0, _FirstNames);
      target.RegisterValues(1, _LastNames);

      var result = (string)target.New();
      Assert.False(string.IsNullOrEmpty(result));
      var resultParts = result.Split(' ');
      Assert.NotNull(resultParts);
      Assert.Equal(2, resultParts.Length);

      Assert.Contains(resultParts[0], _FirstNames);
      Assert.Contains(resultParts[1], _LastNames);
    }
  }
}
