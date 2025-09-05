using DataGenerator.Core;
using System;
using Xunit;

namespace DataGenerator.Tests.Core
{
  public class ValueGeneratorCallRouterTests : IDisposable
  {
    private readonly ValueGeneratorFake _FakeGenerator = new ();

    public ValueGeneratorCallRouterTests()
    {
    }

    public void Dispose()
    {
    }

    #region Helpers

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
        return a+b;
      }

      public int Age1 { get; set; }
      public int Age2 { get; set; }
    }

    private class ValueGeneratorCallRouter_Accessor : ValueGeneratorCallRouter
    {
      public ValueGeneratorCallRouter_Accessor(IValueGenerator generator, double nullProbability)
        : base(generator, nullProbability) { }

      public ValueGeneratorCallRouter_Accessor(IValueGenerator generator, string methodName, double nullProbability)
        : base(generator, methodName, nullProbability) { }

      public ValueGeneratorCallRouter_Accessor(IValueGenerator generator, string methodName, double nullProbability, params object[] parameters)
        : base(generator, methodName, nullProbability, parameters) { }
    }

    #endregion

    #region Constructors

    [Fact]
    public void ValueGeneratorCallRouter_Constructor1_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(null, 0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor1_NegativeProbability()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, -0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor1_ProbabilityTooBig()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, 1.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor1_NoNullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, 0.0);

      Assert.NotNull(target);
      Assert.Same(_FakeGenerator, target.Generator);
      Assert.Equal(0.0, target.ProbabilityOfNull);
      Assert.Equal("New", target.MethodToCall.Name);
      Assert.Null(target.MethodParameters);
      Assert.False(target.IsNullCall);
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor1_NullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, 0.5);

      Assert.True(target.IsNullCall);
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(null!, "MethodNoArgs", 0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_NullMethod()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(_FakeGenerator, null!, 0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_EmptyMethod()
    {
      Assert.Throws<ArgumentException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "", 0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_WhiteSpaceMethod()
    {
      Assert.Throws<ArgumentException>(() => new ValueGeneratorCallRouter(_FakeGenerator, " ", 0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_NegativeProbability()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "MethodNoArgs", -0.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_ProbabilityTooBig()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "MethodNoArgs", 1.5));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_NoNullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, "MethodNoArgs", 0.0);
      Assert.NotNull(target);
      Assert.Same(_FakeGenerator, target.Generator);
      Assert.Equal("MethodNoArgs", target.MethodToCall.Name);
      Assert.Equal(0.0, target.ProbabilityOfNull);
      Assert.Null(target.MethodParameters);
      Assert.False(target.IsNullCall);
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor2_NullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, "MethodNoArgs", 0.5);
      Assert.True(target.IsNullCall);
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NullGenerator()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(null, "MethodWithArgs", 0.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NullMethod()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(_FakeGenerator, null, 0.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_EmptyMethod()
    {
      Assert.Throws<ArgumentException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "", 0.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_WhiteSpaceMethod()
    {
      Assert.Throws<ArgumentException>(() => new ValueGeneratorCallRouter(_FakeGenerator, " ", 0.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NegativeProbability()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "MethodWithArgs", -0.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_ProbabilityTooBig()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "MethodWithArgs", 1.5, new object[] { 1, 2 }));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NullParameters()
    {
      Assert.Throws<ArgumentNullException>(() => new ValueGeneratorCallRouter(_FakeGenerator, "MethodWithArgs", 0.5, null!));
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NoNullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, "MethodWithArgs", 0.0, new object[] { 1, 2 });

      Assert.NotNull(target);
      Assert.Same(_FakeGenerator, target.Generator);
      Assert.Equal("MethodWithArgs", target.MethodToCall.Name);
      Assert.Equal(0.0, target.ProbabilityOfNull);
      Assert.NotNull(target.MethodParameters);
      Assert.Equal(2, target.MethodParameters.Length);
      Assert.Equal(1, target.MethodParameters[0]);
      Assert.Equal(2, target.MethodParameters[1]);
      Assert.False(target.IsNullCall);
    }

    [Fact]
    public void ValueGeneratorCallRouter_Constructor3_NullCall()
    {
      var target = new ValueGeneratorCallRouter_Accessor(_FakeGenerator, "MethodWithArgs", 0.5, new Object[] { 1, 2 });
      Assert.True(target.IsNullCall);
    }

    #endregion    
  }
}
