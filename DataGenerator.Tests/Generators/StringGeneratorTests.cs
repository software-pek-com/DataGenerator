using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class StringGeneratorTests
  {
    #region Default Constructor

    [Fact]
    public void DefaultConstructor()
    {
      new StringGenerator();
    }

    #endregion

    #region Non Default Constructor

    [Fact]
    public void NonDefaultConstructor_InvalidMinLength()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new StringGenerator(-1, 10));
    }

    [Fact]
    public void NonDefaultConstructor_InvalidMaxLength()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new StringGenerator(10, 1));
    }

    [Fact]
    public void NonDefaultConstructor_FixedLength_Ok()
    {
      new StringGenerator(10, 10);
    }

    [Fact]
    public void NonDefaultConstructor_NonFixedLength_Ok()
    {
      new StringGenerator(1, 10);
    }

    #endregion

    #region New

    [Fact]
    public void New_NonFixedLength_GeneratedStringLengthInRange()
    {
      const int minLength = 1;
      const int maxlength = 10;

      var target = new StringGenerator(minLength, maxlength);

      // Small stress test.
      for (int i = 0; i < 100; i++)
      {
          string result = (string) target.New();
          int resultLength = result.Length;

          Assert.True(resultLength >= minLength);
          Assert.True(resultLength <= maxlength);
      }
    }

    #endregion

    #region OfLength

    [Fact]
    public void OfLength_NegativeLength()
    {
      var target = new StringGenerator();

      Assert.Throws<ArgumentOutOfRangeException>(() => target.OfLength(-1));
    }

    [Fact]
    public void OfLength_EmptyString()
    {
      var target = new StringGenerator();

      Assert.Throws<ArgumentOutOfRangeException>(() => target.OfLength(0));
    }

    [Fact]
    public void OfLength_Ok()
    {
      var target = new StringGenerator();

      string result = target.OfLength(123);

      Assert.Equal(123, result.Length);
    }

    #endregion
  }
}
