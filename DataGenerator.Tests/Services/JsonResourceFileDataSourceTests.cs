using DataGenerator.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Xunit;

namespace DataGenerator.Tests.Services
{
  public class JsonResourceFileDataSourceTests
  {
    private const string _ResourceFileName = "JsonResourceFile.json";
    private const string _InvalidResourceFileName = "InvalidJsonResourceFile.json";
    private static readonly Assembly _Assembly = typeof(JsonResourceFileDataSourceTests).Assembly;

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
      Assert.Throws<ArgumentNullException>(() =>
        new JsonResourceFileDataSource<string>(null!, "x"));
    }

    [Fact]
    public void Constructor_NullFile()
    {
      Assert.Throws<ArgumentNullException>(() =>
        new JsonResourceFileDataSource<string>(_Assembly, null!));
    }

    [Fact]
    public void Constructor_EmptyFile()
    {
      Assert.Throws<ArgumentException>(() =>
        new JsonResourceFileDataSource<string>(_Assembly, ""));
    }

    [Fact]
    public void Constructor_WhitespaceFile()
    {
      Assert.Throws<ArgumentException>(() =>
        new JsonResourceFileDataSource<string>(_Assembly, " "));
    }

    [Fact]
    public void Constructor_OK()
    {
      new JsonResourceFileDataSource<PersonData>(_Assembly, _ResourceFileName);
    }

    [Fact]
    public void GetAllValues_ResourceFileNoFound()
    {
      var target = new JsonResourceFileDataSource<PersonData>(_Assembly, "foo");
      Assert.Throws<InvalidOperationException>(() => target.GetAllValues());
    }

    [Fact]
    public void GetAllValues_ResourceFileWithInvalidJson()
    {
      var target = new JsonResourceFileDataSource<PersonData>(_Assembly, _InvalidResourceFileName);
      Assert.Throws<JsonReaderException>(() => target.GetAllValues());
    }

    [Fact]
    public void GetAllValues_OK()
    {
      var target = new JsonResourceFileDataSource<PersonData>(_Assembly, _ResourceFileName);
      var result = target.GetAllValues();

      Assert.NotNull(result);
      Assert.Equal(3, result.Count());
    }
  }
}
