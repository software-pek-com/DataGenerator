namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of dates and times including nulls.
  /// </summary>
  public sealed class DateTimeGenerator : ValueGeneratorBase<DateTime>
  {
    /// <summary>
    /// Age of start of work i.e. 18.
    /// </summary>
    internal static readonly TimeSpan StartOfWorkingAge = new TimeSpan(365 * 18, 0, 0, 0);
    /// <summary>
    /// Age of end of work i.e. 65.
    /// </summary>
    internal static readonly TimeSpan EndOfWorkingAge = new TimeSpan(365 * 65, 0, 0, 0);

    /// <summary>
    /// Returns a new date time between 1900-1-1 and today.
    /// </summary>
    public override DateTime New()
    {
      var start = new DateTime(1900, 1, 1);
      var range = (DateTime.Today - start).Days;
      var randomDays = RandomNumber.Next(range);
      return start.AddDays(randomDays);
    }

    /// <summary>
    /// Returns a new date time in the allowed range i.e [from, to).
    /// Note 'to' is not included in the range.
    /// </summary>
    public DateTime NewDateTime(DateTime from, DateTime to)
    {
      if (to <= from)
      {
        throw new ArgumentException("'from' must be before 'to'.");
      }

      var range = to - from;
      var timeSpan = new TimeSpan((long)(RandomNumber.NextDouble() * range.Ticks));

      return from + timeSpan;
    }

    /// <summary>
    /// Returns a new date time in the allowed range i.e [from, to).
    /// Note 'to' is not included in the range.
    /// </summary>
    public DateTime InRange(DateTime from, DateTime to)
    {
      return NewDateTime(from, to);
    }

    /// <summary>
    /// Returns a new date time in the allowed range i.e [from, to).
    /// The from/to date can be null.
    /// Note 'to' is not included in the range.
    /// </summary>
    public DateTime NewDateTime(DateTime? from, DateTime? to)
    {
      if (from.HasValue && to.HasValue)
      {
        return NewDateTime(from.Value, to.Value);
      }
      else if (!from.HasValue && !to.HasValue)
      {
        return New();
      }
      else if (from.HasValue)
      {
        return NewDateTime(from.Value, from.Value.Add(EndOfWorkingAge));
      }

      return NewDateTime(to!.Value.Add(-EndOfWorkingAge), to.Value);
    }

    /// <summary>
    /// Returns a new date (no time part) in the allowed range i.e [from, to).
    /// Note 'to' is not included in the range.
    /// </summary>
    public DateTime NewDate(DateTime from, DateTime to)
    {
      if (to <= from)
      {
        throw new ArgumentException("'from' must be before 'to'.");
      }

      var range = to.Date - from.Date;
      var timeSpanInDays = (int)(RandomNumber.NextDouble() * range.TotalDays);
      var timeSpan = new TimeSpan(timeSpanInDays, 0, 0, 0);

      return (from + timeSpan).Date;
    }

    /// <summary>
    /// Returns a new time (TimeSpan i.e. time from midnight) in the allowed range i.e [from, to).
    /// Note 'to' is not included in the range.
    /// </summary>
    public TimeSpan NewTime(TimeSpan from, TimeSpan to)
    {
      if (to <= from)
      {
        throw new ArgumentException("'from' must be before 'to'.");
      }

      var range = to - from;
      var timeSpan = new TimeSpan((long)(RandomNumber.NextDouble() * range.TotalDays));

      return from + timeSpan;
    }

    /// <summary>
    /// Returns a new birth date i.e. someone of working age (as of today).
    /// It is assumed that the working age is between 18 and 65.
    /// </summary>
    public DateTime NewBirthDate()
    {
      var now = DateTime.Today;
      var from = now - EndOfWorkingAge;
      var to = now - StartOfWorkingAge;

      return NewDate(from, to);
    }
  }
}
