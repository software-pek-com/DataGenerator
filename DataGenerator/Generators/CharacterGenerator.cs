using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of characters.
  /// </summary>
  public sealed class CharacterGenerator : IValueGenerator
  {
    private static readonly List<char> _CharacterList = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToList();

    /// <summary>
    /// Returns a random character.
    /// </summary>
    public object New()
    {
      var random = new Random();
      int index = random.Next(0, _CharacterList.Count - 1);
      return _CharacterList[index];
    }
  }
}