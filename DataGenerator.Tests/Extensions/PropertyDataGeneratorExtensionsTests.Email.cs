using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsEmailTests
  {
    private const int _NumberOfIterations = 100;

    private class ObjToBuild
    {
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? Email { get; set; }
    }

    [Fact]
    public void AsEmailAddress1_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsEmailAddress());
    }

    [Fact]
    public void AsEmailAddress2_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsEmailAddress(o => o.FirstName!, o => o.LastName!));
    }

    [Fact]
    public void AsEmailAddress2_NullFirstName()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentNullException>(() =>target.For<string>(o => o.Email).AsEmailAddress(null!, o => o.LastName!));
    }

    [Fact]
    public void AsEmailAddress2_NullLastName()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentNullException>(() => target.For<string>(o => o.Email).AsEmailAddress(o => o.FirstName!, null!));
    }

    [Fact]
    public void AsEmailAddress1_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Email).AsEmailAddress();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Email));
    }

    [Fact]
    public void AsEmailAddress2_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.FirstName).OfLength(3);
      target.For<string>(o => o.LastName).OfLength(3);
      
      target.For<string>(o => o.Email).AsEmailAddress(o => o.FirstName!, o => o.LastName!);
      var result = target.Generate();
      
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.FirstName));
      Assert.False(string.IsNullOrEmpty(result.LastName));
      var email = result.Email;
      Assert.False(string.IsNullOrEmpty(email));

      Assert.Contains(result.FirstName!, email!.Contains);
      Assert.Contains(result.LastName!, email.Contains);
    }
  }
}
