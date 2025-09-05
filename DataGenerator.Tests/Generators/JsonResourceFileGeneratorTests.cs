using DataGenerator.Generators;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class JsonResourceFileGeneratorTests
  {
    private const string _ResourceFileName = "JsonResourceFile.json";
    private static readonly Assembly _Assembly = typeof(JsonResourceFileGeneratorTests).Assembly;

    [DataContract]
    internal class PersonData
    {
      [DataMember]
      public string? FirstName { get; set; }

      [DataMember]
      public string? LastName { get; set; }

      [DataMember]
      public int Age { get; set; }
    }

    [Fact]
    public void Constructor_NullAssembly()
    {
      Assert.Throws<ArgumentNullException>(() => new JsonResourceFileGenerator<PersonData>(null!, _ResourceFileName));
    }

    [Fact]
    public void Constructor_NullFile()
    {
      Assert.Throws<ArgumentNullException>(() => new JsonResourceFileGenerator<PersonData>(_Assembly, null!));
    }

    [Fact]
    public void Constructor_EmptyFile()
    {
      Assert.Throws<ArgumentException>(() => new JsonResourceFileGenerator<PersonData>(_Assembly, ""));
    }

    [Fact]
    public void Constructor_WhitelistFile()
    {
      Assert.Throws<ArgumentException>(() => new JsonResourceFileGenerator<PersonData>(_Assembly, " "));
    }

    [Fact]
    public void Constructor_OK()
    {
      new JsonResourceFileGenerator<PersonData>(_Assembly, _ResourceFileName);
    }

    [Fact]
    public void New_OK()
    {
      var target = new JsonResourceFileGenerator<PersonData>(_Assembly, _ResourceFileName);
      var result = target.New();

      Assert.NotNull(result);
    }
  }
}
