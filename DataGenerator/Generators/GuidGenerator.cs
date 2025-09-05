namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a generator of <see cref="Guid"/>s.
  /// </summary>
  public class GuidGenerator : ValueGeneratorBase<Guid>
  {
    /// <summary>
    /// Returns a new <see cref="Guid"/>.
    /// </summary>
    public override Guid New()
    {
      return Guid.NewGuid();
    }
  }
}
