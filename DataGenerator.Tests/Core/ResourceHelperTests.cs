using DataGenerator.Core;
using System;
using System.Reflection;
using Xunit;

namespace DataGenerator.Tests.Core
{
  public class ResourceHelperTests
  {
    private const string ExpectedLorem = "Lorem ipsum dolor sit amet.";

    private class SomeClassInAssembly { }

    private ResourceHelper CreateTarget()
    {
      return new ();
    }

    #region GetText ByOrigin

    [Fact]
    public void GetText_ByObjectOrigin()
    {
      var target = CreateTarget();

      var text = target.GetText(new SomeClassInAssembly(), "Lorem");
      Assert.Equal(ExpectedLorem, text);
    }

    [Fact]
    public void GetText_ByObjectOrigin_ResourceNameIsExact()
    {
      var target = CreateTarget();

      var text = target.GetText(new SomeClassInAssembly(), "LoremIpsum.txt");
      Assert.Equal(ExpectedLorem, text);
    }

    [Fact]
    public void GetText_ByObjectOrigin_ResourceNameInvalid()
    {
      var target = CreateTarget();

      Assert.Throws<InvalidOperationException>(() => target.GetText(new SomeClassInAssembly(), "xxxsds d"));
    }

    [Fact]
    public void GetText_ByObjectOrigin_ResourceNameIsCaseSensitive()
    {
      var target = CreateTarget();

      Assert.Throws<InvalidOperationException>(() => target.GetText(new SomeClassInAssembly(), "lOrem"));
    }

    [Fact]
    public void GetText_ByObjectOrigin_ResourceNameEmpty()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentException>(() => helper.GetText(new SomeClassInAssembly(), ""));
    }

    [Fact]
    public void GetText_ByObjectOrigin_ResourceNameNull()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => helper.GetText(new SomeClassInAssembly(), null!));
    }

    [Fact]
    public void GetText_ByObjectOrigin_ObjectNull()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => helper.GetText((object)null!, "Lorem"));
    }

    #endregion

    #region GetText By Assembly

    [Fact]
    public void GetText_ByAssembly()
    {
      var helper = CreateTarget();

      var text = ResourceHelper.GetText(this.GetType().Assembly, "Lorem");
      Assert.Equal(ExpectedLorem, text);
    }

    [Fact]
    public void GetText_ByAssembly_ResourceNameIsExact()
    {
      var helper = CreateTarget();

      var text = ResourceHelper.GetText(GetType().Assembly, "LoremIpsum.txt");
      Assert.Equal(ExpectedLorem, text);
    }

    [Fact]
    public void GetText_ByAssembly_ResourceNameInvalid()
    {
      var helper = CreateTarget();

      Assert.Throws<InvalidOperationException>(() => ResourceHelper.GetText(this.GetType().Assembly, "xxxsds d"));
    }

    [Fact]
    public void GetText_ByAssembly_ResourceNameIsCaseSensitive()
    {
      var helper = CreateTarget();

      Assert.Throws<InvalidOperationException>(() => ResourceHelper.GetText(GetType().Assembly, "lOrem"));
    }

    [Fact]
    public void GetText_ByAssembly_ResourceNameEmpty()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentException>(() => ResourceHelper.GetText(GetType().Assembly, ""));
    }

    [Fact]
    public void GetText_ByAssembly_ResourceNameNull()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => ResourceHelper.GetText(GetType().Assembly, null!));
    }

    [Fact]
    public void GetText_ByAssembly_AssemblyNull()
    {
      var helper = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => ResourceHelper.GetText((Assembly)null!, "Lorem"));
    }

    #endregion

    #region GetBytes

    [Fact]
    public void GetBytes_WithNullAssembly()
    {
      var target = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => ResourceHelper.GetBytes(null!, "LoremIpsum"));
    }

    [Fact]
    public void GetBytes_WithNullResourceName()
    {
      var target = CreateTarget();

      Assert.Throws<ArgumentNullException>(() => ResourceHelper.GetBytes(GetType().Assembly, null!));
    }

    [Fact]
    public void GetBytes_WithEmptyResourceName()
    {
      var target = CreateTarget();

      Assert.Throws<ArgumentException>(() => ResourceHelper.GetBytes(GetType().Assembly, ""));
    }

    [Fact]
    public void GetBytes_WithWhitespaceResourceName()
    {
      var target = CreateTarget();

      Assert.Throws<ArgumentException>(() => ResourceHelper.GetBytes(GetType().Assembly, "     "));
    }

    [Fact]
    public void GetBytes_WithInvalidResourceName()
    {
      var target = CreateTarget();

      Assert.Throws<InvalidOperationException>(() => ResourceHelper.GetBytes(GetType().Assembly, "Invalid resource name"));
    }

    [Fact]
    public void GetBytes_Ok()
    {
      var target = CreateTarget();

      byte[] result = ResourceHelper.GetBytes(this.GetType().Assembly, "LoremIpsum");

      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

    #endregion
  }
}