using DataGenerator.Generators;

/// <summary>
/// Represents extension methods for the PropertyDataGenerator class.
/// </summary>
public static partial class PropertyDataGeneratorExtensions
{
  private static readonly Lazy<Random> _RandomNumber = new Lazy<Random>(true);
  private static readonly Lazy<BooleanGenerator> _BooleanGenerator = new Lazy<BooleanGenerator>(true);
  private static readonly Lazy<DateTimeGenerator> _DateTimeGenerator = new Lazy<DateTimeGenerator>(true);
  private static readonly Lazy<DoubleGenerator> _DoubleGenerator = new Lazy<DoubleGenerator>(true);
  private static readonly Lazy<DecimalGenerator> _DecimalGenerator = new Lazy<DecimalGenerator>(true);
  private static readonly Lazy<IntegerGenerator> _IntegerGenerator = new Lazy<IntegerGenerator>(true);
  private static readonly Lazy<GuidGenerator> _GuidGenerator = new Lazy<GuidGenerator>(true);
  private static readonly Lazy<StringGenerator> _StringGenerator = new Lazy<StringGenerator>(true);
  private static readonly Lazy<UsernameGenerator> _UsernameGenerator = new Lazy<UsernameGenerator>(true);
  private static readonly Lazy<FirstNameGenerator> _FirstNameGenerator = new Lazy<FirstNameGenerator>(true);
  private static readonly Lazy<FirstNameGenerator> _FemaleFirstNameGenerator = new Lazy<FirstNameGenerator>(() => new FirstNameGenerator(true), true);
  private static readonly Lazy<FirstNameGenerator> _MaleFirstNameGenerator = new Lazy<FirstNameGenerator>(() => new FirstNameGenerator(false), true);
  private static readonly Lazy<LastNameGenerator> _LastNameGenerator = new Lazy<LastNameGenerator>(true);
  private static readonly Lazy<FullNameGenerator> _FullNameGenerator = new Lazy<FullNameGenerator>(true);
  private static readonly Lazy<NamesGenerator> _NamesGenerator = new Lazy<NamesGenerator>(true);
  private static readonly Lazy<SalutationGenerator> _SalutationGenerator = new Lazy<SalutationGenerator>(true);
  private static readonly Lazy<RandomEmailAddressGenerator> _RandomEmailAddressGenerator = new Lazy<RandomEmailAddressGenerator>(true);
  private static readonly Lazy<NameBasedEmailAddressGenerator> _NameBasedEmailAddressGenerator = new Lazy<NameBasedEmailAddressGenerator>(true);
  private static readonly Lazy<CountryNameGenerator> _CountryNameGenerator = new Lazy<CountryNameGenerator>(true);
  private static readonly Lazy<PhoneNumberGenerator> _PhoneNumberGenerator = new Lazy<PhoneNumberGenerator>(true);
  private static readonly Lazy<WebsiteGenerator> _WebsiteGenerator = new Lazy<WebsiteGenerator>(true);
  private static readonly Lazy<CityGenerator> _CityGenerator = new Lazy<CityGenerator>(true);
  private static readonly Lazy<IbanGenerator> _IbanGenerator = new Lazy<IbanGenerator>(true);

  private static readonly Dictionary<string, ResourceFileStringGenerator> _ResourceFileStringGenerators = new Dictionary<string, ResourceFileStringGenerator>();
}
