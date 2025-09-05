using DataGenerator.Core;
using DataGenerator.Generators;
using System;
using System.Reflection;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class PropertyDataGeneratorTests : IDisposable
  {
    private readonly ValueGeneratorFake _FakeGenerator = new ();

    public PropertyDataGeneratorTests() { }

    public void Dispose()
    {
    }

    #region Helpers

    private class ObjToBuild
    {
      public string? Name { get; set; }
    }

    private class PropertyDataGeneratorMock : PropertyDataGenerator<ObjToBuild, string>
    {
      public PropertyDataGeneratorMock(DataGeneratorMock generator, PropertyInfo property)
          : base(generator, property) { }
    }

    private class DataGeneratorMock : DataGenerator<ObjToBuild>
    {
      private DataGeneratorMock() { }

      public static DataGeneratorMock New()
      {
        return new DataGeneratorMock();
      }
    }

    private class ValueGeneratorFake : IValueGenerator
    {
      public object New()
      {
        return "New";
      }

      public string MethodNoArgs()
      {
        return "MethodNoArgs";
      }

      public int MethodWithArgs(int a, int b)
      {
        return a + b;
      }

      public int Age1 { get; set; }
      public int Age2 { get; set; }
    }
    
    private static PropertyInfo GetProperty(string name)
    {
        return typeof(ObjToBuild).GetProperty(name)!;
    }

    #endregion

    [Fact]
    public void Constructor_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => new PropertyDataGeneratorMock(null!, GetProperty("Name")));
    }

    [Fact]
    public void Constructor_NullProperty()
    {
      Assert.Throws<ArgumentNullException>(() => new PropertyDataGeneratorMock(DataGeneratorMock.New(), null!));
    }

    [Fact]
    public void Constructor()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");
      var target = new PropertyDataGeneratorMock(parent, property);

      Assert.NotNull(target);
      Assert.Same(parent, target.Parent);
      Assert.Same(property, target.Property);
    }

    [Fact]
    public void RegisterValueGenerator()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      target.RegisterValueGenerator(_FakeGenerator);

      Assert.Single(parent._ValueGenerators);
      Assert.True(parent._ValueGenerators.ContainsKey(property));
      Assert.Same(_FakeGenerator, parent._ValueGenerators[property].Generator);
    }

    [Fact]
    public void RegisterValueGenerator_WithNegativeProbability()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      Assert.Throws<ArgumentOutOfRangeException>(() => target.RegisterValueGenerator(_FakeGenerator, -0.5));
    }

    [Fact]
    public void RegisterValueGenerator_ProbabilityTooBig()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      Assert.Throws<ArgumentOutOfRangeException>(() => target.RegisterValueGenerator(_FakeGenerator, 1.5));
    }

    [Fact]
    public void RegisterValueGenerator_WithProbability()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      target.RegisterValueGenerator(_FakeGenerator, 0.5);

      Assert.Same(_FakeGenerator, parent._ValueGenerators[property].Generator);
      Assert.Equal(0.5, parent._ValueGenerators[property].ProbabilityOfNull);
    }

    [Fact]
    public void RegisterValueGenerator_NullMethod()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      Assert.Throws<ArgumentNullException>(() => target.RegisterValueGenerator(_FakeGenerator, null!, 0.5));
    }

    [Fact]
    public void RegisterValueGenerator_EmptyMethod()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      Assert.Throws<ArgumentException>(() => target.RegisterValueGenerator(_FakeGenerator, "", 0.5));
    }

    [Fact]
    public void RegisterValueGenerator_WhiteSpaceMethod()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      Assert.Throws<ArgumentException>(() => target.RegisterValueGenerator(_FakeGenerator, " ", 0.5));
    }

    [Fact]
    public void RegisterValueGenerator_WithMethod()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      target.RegisterValueGenerator(_FakeGenerator, "MethodNoArgs", 0.5);

      Assert.Same(_FakeGenerator, parent._ValueGenerators[property].Generator);
      Assert.Equal("MethodNoArgs", parent._ValueGenerators[property].MethodToCall.Name);
      Assert.Equal(0.5, parent._ValueGenerators[property].ProbabilityOfNull);
    }

    [Fact]
    public void RegisterValueGenerator_WithMethodParameters()
    {
      var parent = DataGeneratorMock.New();
      var property = GetProperty("Name");

      var target = new PropertyDataGeneratorMock(parent, property);
      target.RegisterValueGenerator(_FakeGenerator, "MethodWithArgs", 0.5, new object[] { 1, 2 });

      Assert.Same(_FakeGenerator, parent._ValueGenerators[property].Generator);
      Assert.Equal("MethodWithArgs", parent._ValueGenerators[property].MethodToCall.Name);
      Assert.Equal(0.5, parent._ValueGenerators[property].ProbabilityOfNull);
      Assert.Equal(2, parent._ValueGenerators[property].MethodParameters.Length);
      Assert.Equal(1, parent._ValueGenerators[property].MethodParameters[0]);
      Assert.Equal(2, parent._ValueGenerators[property].MethodParameters[1]);
    }
  }
}
