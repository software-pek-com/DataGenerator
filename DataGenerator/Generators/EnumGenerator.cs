using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of enum values for the given enum.
  /// </summary>
  public class EnumGenerator<T> : ValueGeneratorBase<T> where T : struct
  {
    private static readonly List<T> _PossibleEnumValues;
    private readonly IEnumerable<T> _BlacklistValues = Enumerable.Empty<T>();

    static EnumGenerator()
    {
      _PossibleEnumValues = Enum.GetValues(typeof(T)).Cast<T>().ToList();

      if (_PossibleEnumValues.Count == 0)
      {
        throw new InvalidOperationException(
          $"Enum '{typeof(T).Name}' has no defined values.");
      }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public EnumGenerator() { }

    /// <summary>
    /// Initializes an instance of this generator with a black list of enum values.
    /// </summary>
    public EnumGenerator(IEnumerable<T> blacklist)
    {
      Guard.ArgumentNotNullOrEmpty(blacklist, nameof(blacklist));

      _BlacklistValues = blacklist!;
    }

    /// <summary>
    /// Generates a random boolean value.
    /// </summary>
    public override T New()
    {
      if (_BlacklistValues.Any())
      {
        return NewValueNotFromBlackList(_BlacklistValues);
      }

      return InternalNew();
    }

    /// <summary>
    /// Generates a random enum value from a whitelist.
    /// </summary>
    public T NewValueFromWhiteList(IEnumerable<T> whitelist)
    {
      Guard.ArgumentNotNullOrEmpty(whitelist, nameof(whitelist));

      var list = whitelist.ToList();
      var i = RandomNumber.Next(0, list.Count);

      return list[i];
    }

    /// <summary>
    /// Generates a random enum value which is not part of the blacklist.
    /// </summary>
    public T NewValueNotFromBlackList(IEnumerable<T> blacklist)
    {
      Guard.ArgumentNotNullOrEmpty(blacklist, nameof(blacklist));

      var blackListHashset = new HashSet<T>(blacklist!);
      if (blackListHashset.Count == _PossibleEnumValues.Count)
      {
        throw new InvalidOperationException("The blacklist contains all the enum values.");
      }

      return InternalNew(blackListHashset);
    }

    private T InternalNew()
    {
      var i = RandomNumber.Next(0, _PossibleEnumValues.Count);
      return _PossibleEnumValues[i];
    }

    private T InternalNew(HashSet<T> blacklist)
    {
      while (true)
      {
        var generatedValue = InternalNew();
        if (!blacklist.Contains(generatedValue)) { return generatedValue; }
      }
    }
  }
}
