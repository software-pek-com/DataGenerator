using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class ResourceFileStringGeneratorTests
  {
    #region Constructor 1

    [Fact]
    public void Constructor1_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => new ResourceFileStringGenerator((string)null!));
    }

    [Fact]
    public void Constructor1_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(""));
    }

    [Fact]
    public void Constructor1_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator("     "));
    }

    [Fact]
    public void Constructor1()
    {
      var target = new ResourceFileStringGenerator("EmailProviders");

      Assert.NotNull(target.ValueDataSource);
      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    #endregion

    #region Constructor 2

    [Fact]
    public void Constructor2_WithNullParameter()
    {
      Assert.Throws<ArgumentNullException>(() => new ResourceFileStringGenerator((string[])null!));
    }

    [Fact]
    public void Constructor2_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => new ResourceFileStringGenerator(new[] { (string)null! }));
    }

    [Fact]
    public void Constructor2_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(new[] { "" }));
    }

    [Fact]
    public void Constructor2_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(new[] { "     " }));
    }

    [Fact]
    public void Constructor2_WithEmptyList()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(new string[0]));
    }

    [Fact]
    public void Constructor2()
    {
      var target = new ResourceFileStringGenerator(new[] { "Countries", "EmailProviders" });

      Assert.NotNull(target.ValueDataSource);
      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    #endregion

    #region Constructor 3

    [Fact]
    public void Constructor3_WithNullType()
    {
      Assert.Throws<ArgumentNullException>(() => new ResourceFileStringGenerator(null!, "EmailProviders"));
    }

    [Fact]
    public void Constructor3_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => new ResourceFileStringGenerator(AssemblyInfo.Type, (string)null!));
    }

    [Fact]
    public void Constructor3_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(AssemblyInfo.Type, ""));
    }

    [Fact]
    public void Constructor3_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new ResourceFileStringGenerator(AssemblyInfo.Type, "     "));
    }

    [Fact]
    public void Constructor3()
    {
      var target = new ResourceFileStringGenerator(AssemblyInfo.Type, "EmailProviders");

      Assert.NotNull(target.ValueDataSource);
      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    #endregion
  }
}
