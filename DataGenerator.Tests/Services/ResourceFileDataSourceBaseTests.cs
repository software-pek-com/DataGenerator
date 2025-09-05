using DataGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Services
{
  public class ResourceFileDataSourceBaseTests
  {
    #region Helpers

    private class ResourceFileDataSourceBaseMock<T> : ResourceFileDataSourceBase<T>
    {
      public ResourceFileDataSourceBaseMock(string resourceFileName) : base(resourceFileName) { }

      public ResourceFileDataSourceBaseMock(string[] resourceFileNames) : base(resourceFileNames) { }

      public T? AdaptItemReturn {get; set;}

      internal override T AdaptItem(string item)
      {
        return AdaptItemReturn!;
      }

      internal string[] GetStringItems_Accessor(string text)
      {
        return GetStringItems(text);
      }
    }

    #endregion

    #region Constructor 1

    [Fact]
    public void Constructor1_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => 
        new ResourceFileDataSourceBaseMock<string>((string)null!));
    }

    [Fact]
    public void Constructor1_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => 
        new ResourceFileDataSourceBaseMock<string>(""));
    }

    [Fact]
    public void Constructor1_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() =>
        new ResourceFileDataSourceBaseMock<string>("     "));
    }

    [Fact]
    public void Constructor1()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");

      Assert.False(target._Items.IsValueCreated);
    }

    #endregion

    #region Constructor 2

    [Fact]
    public void Constructor2_WithNullParameter()
    {
      Assert.Throws<ArgumentNullException>(() => 
        new ResourceFileDataSourceBaseMock<string>((string[])null!));
    }

    [Fact]
    public void Constructor2_WithNullResourceFileName()
    {
      Assert.Throws<ArgumentNullException>(() => 
        new ResourceFileDataSourceBaseMock<string>(new[] { (string)null! }));
    }

    [Fact]
    public void Constructor2_WithEmptyResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => 
        new ResourceFileDataSourceBaseMock<string>(new[] { "" }));
    }

    [Fact]
    public void Constructor2_WithEmptyList()
    {
      Assert.Throws<ArgumentException>(() => 
        new ResourceFileDataSourceBaseMock<string>(new string[0]));
    }

    [Fact]
    public void Constructor2_WithWhiteSpaceResourceFileName()
    {
      Assert.Throws<ArgumentException>(() => 
        new ResourceFileDataSourceBaseMock<string>(new[] { "     " }));
    }

    [Fact]
    public void Constructor2()
    {
      var target = new ResourceFileDataSourceBaseMock<string>(new[] { "Countries", "WebsiteExtensions" });

      Assert.False(target._Items.IsValueCreated);
    }

    #endregion

    #region GetAllValues

    [Fact]
    public void GetAllValues()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");
      string adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;

      IEnumerable<string> result = target.GetAllValues();

      Assert.True(target._Items.IsValueCreated);

      foreach(var resultItem in result)
      {
        Assert.Equal(adaptedItemValue, resultItem);
      }
    }

    [Fact]
    public void GetAllValues_WithTwoResourceFileNames()
    {
      var target = new ResourceFileDataSourceBaseMock<string>(new[] { "Countries", "WebsiteExtensions" });
      string adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;

      IEnumerable<string> result = target.GetAllValues();

      Assert.True(target._Items.IsValueCreated);

      foreach (var resultItem in result)
      {
        Assert.Equal(adaptedItemValue, resultItem);
      }
    }

    [Fact]
    public void GetAllValues_CallTwiceDoesNotLoadItemsAgain()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");
      string adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;

      target.GetAllValues(); // Call first to lazy load the items

      target.GetAllValues();
    }

    #endregion

    #region GetValues

    [Fact]
    public void GetValues_ArgumentSmallerThan1()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");

      Assert.Throws<ArgumentOutOfRangeException>(() => target.GetValues(0));
    }

    [Fact]
    public void GetValues_ArgumentLargerThanNumberOfItems()
    {
      var target = new ResourceFileDataSourceBaseMock<string>(new[] { "Countries", "WebsiteExtensions" });
      var adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;
      var numberOfValuesToTake = target._Items.Value.Count() + 10;

      var result = target.GetValues(numberOfValuesToTake);

      Assert.Equal(target._Items.Value.Count(), result.Count());
    }

    [Fact]
    public void GetValues()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");
      var adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;
      var numberOfValues = 2;

      var result = target.GetValues(numberOfValues);

      Assert.True(target._Items.IsValueCreated);
      Assert.Equal(numberOfValues, result.Count());

      foreach (var resultItem in result)
      {
        Assert.Equal(adaptedItemValue, resultItem);
      }
    }

    [Fact]
    public void GetValues_WithTwoResourceFileNames()
    {
      var target = new ResourceFileDataSourceBaseMock<string>(new[] { "Countries", "WebsiteExtensions" });
      var adaptedItemValue = "adapted value";
      target.AdaptItemReturn = adaptedItemValue;
      var numberOfValues = 2;

      var result = target.GetValues(numberOfValues);

      Assert.True(target._Items.IsValueCreated);
      Assert.Equal(numberOfValues, result.Count());

      foreach (var resultItem in result)
      {
        Assert.Equal(adaptedItemValue, resultItem);
      }
    }

    #endregion

    #region GetStringItems

    [Fact]
    public void GetStringItems_SplitOnNewLine()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");

      var sourceText = string.Format("{0}{2}{1}", "Value1", "Value2", "\n");
      var result = target.GetStringItems_Accessor(sourceText);

      string expectedValue1 = "Value1";
      string expectedValue2 = "Value2";
      Assert.Equal(2, result.Count());
      Assert.Equal(expectedValue1, result[0]);
      Assert.Equal(expectedValue2, result[1]);
    }

    [Fact]
    public void GetStringItems_TrimCarriageReturn()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");

      var sourceText = "\rValue1\nValue2\r\n";
      var result = target.GetStringItems_Accessor(sourceText);

      string expectedValue1 = "Value1";
      string expectedValue2 = "Value2";
      Assert.Equal(2, result.Count());
      Assert.Equal(expectedValue1, result[0]);
      Assert.Equal(expectedValue2, result[1]);
    }

    [Fact]
    public void GetStringItems_ResultLinesTrimmed()
    {
      var target = new ResourceFileDataSourceBaseMock<string>("WebsiteExtensions");

      var sourceText = "Value1   \n   Value2\n";
      var result = target.GetStringItems_Accessor(sourceText);

      string expectedValue1 = "Value1";
      string expectedValue2 = "Value2";
      Assert.Equal(2, result.Count());
      Assert.Equal(expectedValue1, result[0]);
      Assert.Equal(expectedValue2, result[1]);
    }

    #endregion
  }
}
