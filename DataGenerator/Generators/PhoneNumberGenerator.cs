namespace DataGenerator.Generators
{
  /// <summary>
  /// This generator generates random phone numbers.
  /// </summary>
  public class PhoneNumberGenerator : ValueGeneratorBase<string>
  {
    /// <summary>
    /// Generates a new phone number.
    /// </summary>
    public override string New()
    {
      int prefixLength = GetPartLength();
      int maxPrefixNumber = GetMaxPartNumber(prefixLength);
      string prefix = GetPart(prefixLength, maxPrefixNumber);

      int partLength = GetPartLength();
      int maxPartNumber = GetMaxPartNumber(partLength);
      string part1 = GetPart(partLength, maxPartNumber);
      string part2 = GetPart(partLength, maxPartNumber);
      string part3 = GetPart(partLength, maxPartNumber);

      var result = $"+{prefix}.{part1}{part2}{part3}";

      return result;
    }

    private int GetPartLength() { return RandomNumber.Next(2, 4); }

    private int GetMaxPartNumber(int partLength)
    {
      var result = (int)Math.Pow(10, partLength);
      return result;
    }

    private string GetPart(int partLength, int maxPartNumber)
    {
      return RandomNumber.Next(1, maxPartNumber).ToString("D" + partLength);
    }
  }
}
