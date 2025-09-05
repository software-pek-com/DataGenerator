using DataGenerator.Generators;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsStringTests
  {
    private class ObjToBuild
    {
      public string? Name { get; set; }
      public string? AnotherName { get; set; }
    }

    [Fact]
    public void OfLength_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, string>)null!).OfLength(1));
    }

    [Fact]
    public void AsFirstName1_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsFirstName(true));
    }

    [Fact]
    public void AsFirstName2_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsFirstName(0.0));
    }

    [Fact]
    public void AsFirstName2_NegativeProbability()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<string>(o => o.Name).AsFirstName(-0.1));
    }

    [Fact]
    public void AsFirstName2_ProbabilityTooBig()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<string>(o => o.Name).AsFirstName(1.1));
    }

    [Fact]
    public void AsFirstName3_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => 
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsFirstName());
    }

    [Fact]
    public void AsLastName_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => 
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsLastName());
    }

    [Fact]
    public void AsUsername1_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => 
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsUsername());
    }

    [Fact]
    public void AsUsername2_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => 
        ((PropertyDataGenerator<ObjToBuild, string>)null!).AsUsername(3));
    }

    [Fact]
    public void AsUsername2_NegativeLength()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<string>(o => o.Name).AsUsername(-1));
    }

    [Fact]
    public void AsUsername2_ZeroLength()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        target.For<string>(o => o.Name).AsUsername(0));
    }

    [Fact]
    public void OfLength_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).OfLength(3);
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
      Assert.Equal(3, result.Name!.Length);
    }

    [Fact]
    public void AsFirstName_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsFirstName();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsFirstName2_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsFirstName(0.5);
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsFirstName3_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsFirstName(false);
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsLastName_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsLastName();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsUsername1_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsUsername();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
      Assert.Equal(4, result.Name!.Length);
    }

    [Fact]
    public void AsUsername2_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsUsername(13);
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
      Assert.Equal(13, result.Name!.Length);
    }

    [Fact]
    public void AsCountry_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsCountry();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsPhoneNumber_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsPhoneNumber();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsIban_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsIban();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsWebsite_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsWebsite();
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsCity_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.AnotherName).AsNew(() => "belgium");
      target.For<string>(o => o.Name).AsCity(o => o.AnotherName);
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsResourceFileString_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsResourceFileString("Countries");
      var result = target.Generate();
      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }

    [Fact]
    public void AsResourceFileString_OnlyOneGeneratorPerResourceFileName_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      string resourceFileName = "Countries";
      target.For<string>(o => o.AnotherName).AsResourceFileString(resourceFileName);
      target.For<string>(o => o.Name).AsResourceFileString(resourceFileName);
      var result = target.Generate();

      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
      Assert.False(string.IsNullOrEmpty(result.AnotherName));

      var namePropertyDataGenerator = target.For<string>(o => o.Name);
      var nameDataGenerator = namePropertyDataGenerator.Parent._ValueGenerators
        .Single(vg => vg.Key.Name.Equals("Name", StringComparison.InvariantCultureIgnoreCase)).Value.Generator;

      var anotherNamePropertyDataGenerator = target.For<string>(o => o.AnotherName);
      var anotherNameDataGenerator = anotherNamePropertyDataGenerator.Parent._ValueGenerators
        .Single(vg => vg.Key.Name.Equals("AnotherName", StringComparison.InvariantCultureIgnoreCase)).Value.Generator;

      Assert.Same(nameDataGenerator, anotherNameDataGenerator);
    }

    [Fact]
    public void AsResourceFileString2_NullType()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentNullException>(() => 
        target.For<string>(o => o.Name).AsResourceFileString(null!, "Countries"));
    }

    [Fact]
    public void AsResourceFileString2_WithNullResourceFileName()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentNullException>(() => 
        target.For<string>(o => o.Name).AsResourceFileString(AssemblyInfo.Type, (string)null!));
    }

    [Fact]
    public void AsResourceFileString2_WithEmptyResourceFileName()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentException>(() => 
        target.For<string>(o => o.Name).AsResourceFileString(AssemblyInfo.Type, ""));
    }

    [Fact]
    public void AsResourceFileString2_WithWhiteSpaceResourceFileName()
    {
      var target = new DataGenerator<ObjToBuild>();
      Assert.Throws<ArgumentException>(() => 
        target.For<string>(o => o.Name).AsResourceFileString(AssemblyInfo.Type, "     "));
    }

    [Fact]
    public void AsResourceFileString2_OK()
    {
      var target = new DataGenerator<ObjToBuild>();
      target.For<string>(o => o.Name).AsResourceFileString(AssemblyInfo.Type, "Countries");

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.False(string.IsNullOrEmpty(result.Name));
    }
  }
}
