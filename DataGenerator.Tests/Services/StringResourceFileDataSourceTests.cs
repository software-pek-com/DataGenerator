using DataGenerator.Services;
using System;
using Xunit;

namespace DataGenerator.Tests.Services
{
  public class StringResourceFileDataSourceTests
  {
    #region Constructor 1

    [Fact]
    public void Constructor1_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => 
        new StringResourceFileDataSource((string)null!));
    }

    [Fact]
    public void Constructor1_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new StringResourceFileDataSource(""));
    }

    [Fact]
    public void Constructor1_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new StringResourceFileDataSource("     "));
    }

    [Fact]
    public void Constructor1()
    {
      new StringResourceFileDataSource("WebsiteExtensions");
    }

    #endregion

    #region Constructor 2

    [Fact]
    public void Constructor2_WithNullParameter()
    {
      Assert.Throws<ArgumentNullException>(() => new StringResourceFileDataSource((string[])null!));
    }

    [Fact]
    public void Constructor2_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => new StringResourceFileDataSource(new[] { (string)null! }));
    }

    [Fact]
    public void Constructor2_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new StringResourceFileDataSource(new[] { "" }));
    }

    [Fact]
    public void Constructor2_WithEmptyList()
    {
      Assert.Throws<ArgumentException>(() => new StringResourceFileDataSource(new string[0]));
    }

    [Fact]
    public void Constructor2_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => new StringResourceFileDataSource(new[] { "     " }));
    }

    [Fact]
    public void Constructor2()
    {
      new StringResourceFileDataSource(new[] { "Countries", "WebsiteExtensions" });
    }

    #endregion

    #region AdaptItem

    [Fact]
    public void AdaptItem_WithNullItem()
    {
      var target = new StringResourceFileDataSource("WebsiteExtensions");

      var result = target.AdaptItem(null!);

      Assert.Null(result);
    }

    [Fact]
    public void AdaptItem_WithEmptyItem()
    {
      var target = new StringResourceFileDataSource("WebsiteExtensions");

      var result = target.AdaptItem(string.Empty);

      Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AdaptItem()
    {
      var target = new StringResourceFileDataSource("WebsiteExtensions");

      string itemToAdapt = "item to adapt";

      var result = target.AdaptItem(itemToAdapt);

      Assert.Equal(itemToAdapt, result);
    }
    
    #endregion
  }
}
