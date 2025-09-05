using DataGenerator.Core;
using DataGenerator.Generators;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class DataGeneratorTest
  {
    #region Mocks and Accessors

    private class ObjToBuild
    {
      public string Name { get; set; }
      public bool IsItForReal { get; set; }
      public Guid? Id { get; set; }
    }

    private class DataGeneratorMock : DataGenerator<ObjToBuild>
    {
      private DataGeneratorMock() { }

      public new static DataGeneratorMock New()
      {
        return new DataGeneratorMock();
      }
    }

    private static PropertyInfo GetPropertyInfo<TResult>(Expression<Func<ObjToBuild, TResult>> propertyExpression)
    {
      var propertyName = PropertyName.For(propertyExpression);
      return typeof(ObjToBuild).GetProperty(propertyName)!;
    }

    #endregion

    [Fact]
    public void New()
    {
      var target = DataGeneratorMock.New();
      Assert.NotNull(target);
    }

    [Fact]
    public void For_NullProperty()
    {
      var target = DataGeneratorMock.New();
      Assert.Throws<ArgumentNullException>(() => target.For<int>(null!));
    }

    [Fact]
    public void For_SamePropertyTwice()
    {
      var target = DataGeneratorMock.New();

      target.For<bool>(o => o.IsItForReal).AsFalse();
      Assert.Throws<InvalidOperationException>(() => target.For<bool>(o => o.IsItForReal).AsTrue());
    }

    [Fact]
    public void For_OK()
    {
      var target = DataGeneratorMock.New();
      var expectedProperty = GetPropertyInfo(o => o.IsItForReal);
      var result = target.For<bool>(o => o.IsItForReal);

      Assert.NotNull(result);
      Assert.Equal(expectedProperty, result.Property);
      Assert.Same(target, result.Parent);
    }

    [Fact]
    public void Generate_One()
    {
      var target = DataGeneratorMock.New();
      target.For<bool>(o => o.IsItForReal);
      var result = target.Generate();

      Assert.NotNull(result);
      Assert.Same(result.GetType(), typeof(ObjToBuild));
    }

    [Fact]
    public void Generate_Many()
    {
      var target = DataGeneratorMock.New();
      target.For<bool>(o => o.IsItForReal);
      var results = target.Generate(13);

      Assert.NotNull(results);
      Assert.Equal(13, results.Count());
      Assert.True(results.All(r => r.GetType() == typeof(ObjToBuild)));
    }
  }
}
