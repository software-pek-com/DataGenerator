using DataGenerator.Core;
using DataGenerator.Generators;
using Xunit;

namespace DataGenerator.Tests.Extensions
{
  public class PropertyDataGeneratorExtensionsIComplexGeneratorTests
  {
    private class ParentObj
    {
      public Guid Id { get; set; }
    }

    private class ChildObj
    {
      public Guid Id { get; set; }

      public Guid MotherId { get; set; } // FK
      public ParentObj? Mother { get; set; } // Resolved FK

      public Guid FatherId { get; set; } // FK
      public ParentObj? Father { get; set; } // Resolved FK
    }

    private class ParentGeneratorMock : DataGenerator<ParentObj>
    {
      protected override ParentObj CreateNew()
      {
        throw new NotImplementedException();
      }

      protected override void OnBeforeFinalize(ParentObj obj)
      {
        throw new NotImplementedException();
      }
    }

    private class ChildGeneratorMock : DataGenerator<ChildObj>
    {
      protected override ChildObj CreateNew()
      {
        throw new NotImplementedException();
      }

      protected override void OnBeforeFinalize(ChildObj obj)
      {
        throw new NotImplementedException();
      }
    }

    private PropertyDataGenerator<ChildObj, ParentObj> CreatePropertyGenerator()
    {
      var propertyName = PropertyName.For<ChildObj>(o => o.Mother!);
      return new PropertyDataGenerator<ChildObj, ParentObj>(
          new ChildGeneratorMock(), typeof(ChildObj).GetProperty(propertyName)!);
    }

    [Fact]
    public void AsGenerator()
    {
      var target = new ChildGeneratorMock();
      Assert.NotNull(target);
    }

    [Fact]
    public void AsGenerator1_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ChildObj, ParentObj>)null!).AsGenerator(new ParentGeneratorMock()));
    }

    [Fact]
    public void AsGenerator1_OK()
    {
      var propGen = CreatePropertyGenerator();
      var generator = new ParentGeneratorMock();

      Assert.Empty(generator._ComplexGenerators);

      propGen.AsGenerator(generator);

      Assert.Single(propGen.Parent._ComplexGenerators);
      Assert.Equal(propGen.Property, propGen.Parent._ComplexGenerators.Single().Key);
      Assert.Same(generator, propGen.Parent._ComplexGenerators.Single().Value);
    }

    [Fact]
    public void AsGenerator2_NullPropertyDataGenerator()
    {
      Assert.Throws<ArgumentNullException>(() =>
        ((PropertyDataGenerator<ChildObj, ParentObj>)null!).AsGenerator(new ParentGeneratorMock(), o => o.ToString()));
    }

    [Fact]
    public void AsGenerator2_NullDataGenerator()
    {
      var propGen = CreatePropertyGenerator();

      Assert.Throws<ArgumentNullException>(() => propGen.AsGenerator(null!, o => o.ToString()));
    }

    [Fact]
    public void AsGenerator2_NullFinalizerAction()
    {
      var propGen = CreatePropertyGenerator();

      Assert.Throws<ArgumentNullException>(() =>
        propGen.AsGenerator(new ParentGeneratorMock(), (Action<ParentObj>)null!));
    }

    [Fact]
    public void AsGenerator2_OK()
    {
      var propGen = CreatePropertyGenerator();
      var generator = new ParentGeneratorMock();

      Assert.Empty(generator._ComplexGenerators);

      propGen.AsGenerator(generator, o => o.ToString());

      Assert.Single(propGen.Parent._ComplexGenerators);
      Assert.Equal(propGen.Property, propGen.Parent._ComplexGenerators.Single().Key);
      Assert.Same(generator, propGen.Parent._ComplexGenerators.Single().Value);
    }
  }
}
