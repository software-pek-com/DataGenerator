using DataGenerator.Generators;
using System.Linq;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class FirstNameGeneratorTests
  {
    [Fact]
    public void Constructor_IsFemale()
    {
      var target = new FirstNameGenerator(true);
      Assert.NotNull(target);

      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    [Fact]
    public void Constructor_IsMale()
    {
      var target = new FirstNameGenerator(false);
      Assert.NotNull(target);

      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    [Fact]
    public void Constructor_All()
    {
      var target = new FirstNameGenerator();
      Assert.NotNull(target);

      Assert.NotEmpty(target.ValueDataSource.GetAllValues());
    }

    [Fact]
    public void ParameterValueMaps()
    {
      var targetAll = new FirstNameGenerator();
      var targetFemale = new FirstNameGenerator(true);
      var targetMale = new FirstNameGenerator(false);

      var allNames = targetAll.ValueDataSource.GetAllValues();
      var femaleNames = targetFemale.ValueDataSource.GetAllValues();
      var maleNames = targetMale.ValueDataSource.GetAllValues();

      Assert.Equal(allNames.Count(), femaleNames.Count() + maleNames.Count());
    }

    [Fact]
    public void NewValue_AllNames()
    {
      var target = new FirstNameGenerator(true);

      var possibleValues = target.ValueDataSource.GetAllValues();

      Assert.Contains("Eve", possibleValues);
      Assert.Contains("Adam", possibleValues);
    }

    [Fact]
    public void NewValue_FemaleNames()
    {
      var target = new FirstNameGenerator(true);

      var possibleValues = target.ValueDataSource.GetAllValues();
      Assert.Contains("Eve", possibleValues);
    }

    [Fact]
    public void NewValue_MaleNames()
    {
      var target = new FirstNameGenerator(false);

      var possibleValues = target.ValueDataSource.GetAllValues();
      Assert.Contains("Adam", possibleValues);
    }
  }
}
