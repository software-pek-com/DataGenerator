using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class FormatDescriptorTests
  {
    [Fact]
    public void Constructor_NullFormat()
    {
      Assert.Throws<ArgumentNullException>(() => new FormatDescriptor(null!));
    }

    [Fact]
    public void Constructor_EmptyFormat()
    {
      Assert.Throws<ArgumentException>(() => new FormatDescriptor(""));
    }

    [Fact]
    public void Constructor_WhiteSpaceFormat()
    {
      Assert.Throws<ArgumentException>(() => new FormatDescriptor(" "));
    }

    [Fact]
    public void Constructor_NoParameters()
    {
      Assert.Throws<InvalidOperationException>(() => new FormatDescriptor("X"));
    }

    [Fact]
    public void Constructor_OK()
    {
      var expectedPattern = "{0} {1}";
      var target = new FormatDescriptor(expectedPattern);

      Assert.Equal(2, target.ParameterCount);
      Assert.Equal(expectedPattern, target.Pattern);
    }

    [Fact]
    public void FormatPattern_NullFormat()
    {
      var target = new FormatDescriptor();
      Assert.Throws<ArgumentNullException>(() => target.Pattern = null!);
    }

    [Fact]
    public void FormatPattern_EmptyFormat()
    {
      var target = new FormatDescriptor();
      Assert.Throws<ArgumentException>(() => target.Pattern = "");
    }

    [Fact]
    public void FormatPattern_WhiteSpaceFormat()
    {
      var target = new FormatDescriptor();
      Assert.Throws<InvalidOperationException>(() => target.Pattern = " ");
    }

    [Fact]
    public void FormatPattern_NoParameters()
    {
      var target = new FormatDescriptor();
      Assert.Throws<InvalidOperationException>(() => target.Pattern = "X");
    }

    [Fact]
    public void FormatPattern_OK()
    {
      var target = new FormatDescriptor();

      var expectedPattern = "{0} {1}";
      target.Pattern = expectedPattern;

      Assert.Equal(2, target.ParameterCount);
      Assert.Equal(expectedPattern, target.Pattern);
    }
  }
}
