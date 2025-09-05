using DataGenerator.Core;
using System;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class ComplexGeneratorBaseTests : IDisposable
  {
    private static Guid _ExpectedParentId;

    public ComplexGeneratorBaseTests()
    {
      _ExpectedParentId = Guid.NewGuid();
    }

    public void Dispose() { }

    private class ParentObj
    {
      public Guid Id { get; set; }
    }

    private class ChildObj
    {
      public Guid Id { get; set; }

      public Guid MotherId { get; set; } // FK
      public ParentObj Mother { get; set; } // Resolved FK

      public Guid FatherId { get; set; } // FK
      public ParentObj Father { get; set; } // Resolved FK
    }

    private class ComplexGeneratorMock : ComplexGeneratorBase<ParentObj>
    {
      protected override ParentObj CreateNew()
      {
        return new ParentObj { Id = _ExpectedParentId };
      }
    }

    private class NonPublishingComplexGeneratorMock : ComplexGeneratorBase<ParentObj>
    {
      protected override ParentObj CreateNew()
      {
        return new ParentObj { Id = _ExpectedParentId };
      }
    }

    private ComplexGeneratorMock CreateTarget()
    {
      return new ComplexGeneratorMock();
    }

    [Fact]
    public void ComplexGeneratorBase_Constructor()
    {
      CreateTarget();
    }

    [Fact]
    public void ComplexGeneratorBase_Generate_WithoutFinalizers()
    {
      var target = CreateTarget();
      var result = target.Generate();

      Assert.NotNull(result);
      Assert.False(result.Id == Guid.Empty);
    }

    [Fact]
    public void ComplexGeneratorBase_Generate_CallsFinalizers()
    {
      var target = CreateTarget();
      var finalizerCalled = false;
      target.Finalize += o => { Assert.NotNull(o); finalizerCalled = true; };

      var result = target.Generate();
      
      Assert.True(finalizerCalled);
    }

    [Fact]
    public void ComplexGeneratorBase_Generate2_OK()
    {
      var target = CreateTarget();

      var results = target.Generate(3);

      Assert.NotNull(results);
      Assert.Equal(3, results.Count());
    }

    [Fact]
    public void ComplexGeneratorBase_CreateNew()
    {
      var target = CreateTarget();

      var result = target.Generate();

      Assert.NotNull(result);
      Assert.False(result.Id == Guid.Empty);
      Assert.Equal(_ExpectedParentId, result.Id);
    }
  }
}
