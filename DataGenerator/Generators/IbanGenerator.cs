namespace DataGenerator.Generators
{
  /// <summary>
  /// This generator generates IBANs.
  /// </summary>
  public class IbanGenerator : ValueGeneratorBase<string>
  {
    /// <summary>
    /// Generates a new random IBAN.
    /// </summary>
    public override string New()
    {
      var countryPrefix = new StringGenerator().OfLength(2).ToUpper();
      var controlNumber = RandomNumber.Next(0, 100).ToString("D2");
      var prefix = $"{countryPrefix}{controlNumber}";

      var partList = new List<string>();
      for (int i = 0; i < 5; ++i)
      {
        var part = RandomNumber.Next(0, 10000).ToString("D4");
        partList.Add(part);
      }
      var parts = string.Join(" ", partList);

      return $"{prefix} {parts}";
    }
  }
}
