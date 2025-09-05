using DataGenerator.Core;

namespace DataGenerator.Tests.Mocks
{
  /// <summary>
  /// Mock for <see cref="IComplexGenerator"/>.
  /// </summary>
  internal class ComplexGeneratorMock<T> : IComplexGenerator<T>
       where T : class, new()
  {
    public int ItemCount { get; private set; }
    public T[]? GenerateReturn { get; set; }

    public T Generate()
    {
      return GenerateReturn![ItemCount++];
    }

    public event GeneratorFinalizer<T>? Finalize;
  }
}
