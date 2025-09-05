using DataGenerator.Generators;
using System;
using Xunit;

namespace DataGenerator.Tests.Generators
{
  public class DateTimeGeneratorTests
  {
    [Fact]
    public void Constructor()
    {
      var target = new DateTimeGenerator();
      Assert.NotNull(target);
    }

    [Fact]
    public void NewDateTime_FromAfterTo()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Now;
      var to = from - new TimeSpan(1, 0, 0);

      Assert.Throws<ArgumentException>(() => target.NewDateTime(from, to));
    }

    [Fact]
    public void NewValue()
    {
      var target = new DateTimeGenerator();
      var result = target.New();
      Assert.NotNull(target);
      Assert.Equal(typeof(DateTime), result.GetType());
    }

    [Fact]
    public void NewDateTime()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Now;
      var to = from + new TimeSpan(1, 0, 0);

      var result = target.NewDateTime(from, to);

      Assert.True(from <= result && result < to);
    }

    [Fact]
    public void NewDateTime_FromToEqual()
    {
      var target = new DateTimeGenerator();
      var now = DateTime.Now;

      Assert.Throws<ArgumentException>(() => target.NewDateTime(now, now));
    }

    [Fact]
    public void NewDate_FromToEqual()
    {
      var target = new DateTimeGenerator();
      var today = DateTime.Today;

      Assert.Throws<ArgumentException>(() => target.NewDateTime(today, today));
    }

    [Fact]
    public void NewTime_FromToEqual()
    {
      var target = new DateTimeGenerator();
      var now = DateTime.Now.TimeOfDay;

      Assert.Throws<ArgumentException>(() => target.NewTime(now, now));
    }

    [Fact]
    public void NewDate_FromAfterTo()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Today;
      var to = from - new TimeSpan(1, 0, 0);

      Assert.Throws<ArgumentException>(() => target.NewDate(from, to));
    }

    [Fact]
    public void NewDate()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Today;
      var to = from + new TimeSpan(3, 1, 0, 0);

      var result = target.NewDate(from, to);

      Assert.True(from <= result && result < to);
    }

    [Fact]
    public void NewTime_FromAfterTo()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Now.TimeOfDay;
      var to = from - new TimeSpan(1, 0, 0);

      Assert.Throws<ArgumentException>(() => target.NewTime(from, to));
    }

    [Fact]
    public void NewTime()
    {
      var target = new DateTimeGenerator();
      var from = DateTime.Now.TimeOfDay;
      var to = from + new TimeSpan(1, 0, 0);

      var result = target.NewTime(from, to);

      Assert.True(from <= result && result < to);
    }

    [Fact]
    public void NewBirthDate()
    {
      var target = new DateTimeGenerator();
      var now = DateTime.Now;

      var result = target.NewBirthDate();

      Assert.True(now - DateTimeGenerator.EndOfWorkingAge <= result
          && result < now - DateTimeGenerator.StartOfWorkingAge);
    }

    [Fact]
    public void NewDateTime_WithNullableDates_WithFrom_WithTo()
    {
      var target = new DateTimeGenerator();
      DateTime? from = DateTime.Now;
      DateTime? to = from.Value + new TimeSpan(1, 0, 0);

      var result = target.NewDateTime(from, to);

      Assert.True(from <= result && result < to);
    }

    [Fact]
    public void NewDateTime_WithNullableDates_WithFrom()
    {
      var target = new DateTimeGenerator();
      DateTime? from = DateTime.Now;

      var result = target.NewDateTime(from, null);

      Assert.True(from <= result);
    }

    [Fact]
    public void NewDateTime_WithNullableDates_WithTo()
    {
      var target = new DateTimeGenerator();
      DateTime? from = null;
      DateTime? to = DateTime.Now + new TimeSpan(1, 0, 0);

      var result = target.NewDateTime(from, to);

      Assert.True(result < to);
    }

    [Fact]
    public void NewDateTime_WithNullableDates_FromToEqual()
    {
      var target = new DateTimeGenerator();
      DateTime? now = DateTime.Now;

      Assert.Throws<ArgumentException>(() => target.NewDateTime(now, now));
    }
  }
}
