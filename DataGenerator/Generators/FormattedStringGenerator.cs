using DataGenerator.Core;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a base class for formatted text generators.
  /// </summary>
  public class FormattedStringGenerator : ValueGeneratorBase<string>
  {
    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal readonly Dictionary<int, object> _ParameterValueMap = new Dictionary<int, object>();

    public FormatDescriptor Descriptor { get; }

    /// <summary>
    /// Registers possible values for a parameter.
    /// </summary>
    protected void RegisterValues(int parameter, IValueGenerator values)
    {
      Guard.ArgumentInRange(0, Descriptor.ParameterCount - 1, parameter, nameof(parameter));
      Guard.ArgumentNotNull(values, nameof(values));

      if (!(values is IValueGenerator || values is IList<string>))
      {
        throw new ArgumentException(
            $"Invalid type of values '{values.GetType()}' for parameter '{parameter}'.");
      }
      else if (values is IList<string>)
      {
        if (((IList<string>)values).Count == 0)
        {
          throw new ArgumentException(
              $"Cannot accept empty lists of possible values for parameter '{parameter}'.");
        }
      }

      _ParameterValueMap.Add(parameter, values);
    }

    /// <summary>
    /// Registers possible values for a parameter.
    /// </summary>
    protected void RegisterValues(int parameter, IList<string> values)
    {
      Guard.ArgumentInRange(0, Descriptor.ParameterCount - 1, parameter, nameof(parameter));
      Guard.ArgumentNotNull(values, nameof(values));

      if (values.Count == 0)
      {
        throw new ArgumentException(
            $"Cannot accept empty lists of possible values for parameter '{parameter}'.");
      }

      _ParameterValueMap.Add(parameter, values);
    }
    /// <summary>
    /// Initializes an instance of this class with the given string format.
    /// </summary>
    public FormattedStringGenerator(string format)
      : this(new FormatDescriptor(format)) { }

    /// <summary>
    /// Initializes an instance of this class with the given format descriptor.
    /// </summary>
    public FormattedStringGenerator(FormatDescriptor descriptor)
    {
      Guard.ArgumentNotNull(descriptor, nameof(descriptor));

      Descriptor = descriptor;
    }

    /// <summary>
    /// Generates a new value based on the format descriptor and registered values.
    /// </summary>
    public override string New()
    {
      VerifyParameterMap();

      var parameterCount = Descriptor.ParameterCount;
      var chosenParts = new List<string>(parameterCount);

      for (int i = 0; i < parameterCount; ++i)
      {
        chosenParts.Add(ChooseValue(i));
      }

      return string.Format(Descriptor.Pattern, chosenParts.ToArray());
    }

    private void VerifyParameterMap()
    {
      for (int i = 0; i < Descriptor.ParameterCount; ++i)
      {
        if (!_ParameterValueMap.ContainsKey(i))
        {
          throw new InvalidOperationException(
              $"Missing mapping for parameter '{i}'.");
        }
      }
    }

    private object ResolveParameter(int parameter)
    {
      if (!_ParameterValueMap.ContainsKey(parameter))
      {
        throw new InvalidOperationException(
            $"Format parameter '{parameter}' has no value mapping.");
      }

      return _ParameterValueMap[parameter];
    }

    /// <summary>
    /// Chooses one of the possible values for the given parameter.
    /// </summary>
    protected string ChooseValue(int parameter)
    {
      var possibleValues = ResolveParameter(parameter);

      object result;
      if (possibleValues is IValueGenerator)
      {
        var possibleDataGenerator = (IValueGenerator)possibleValues;
        result = possibleDataGenerator.New();
      }
      else // possibleValues is an IList<string>
      {
        var possibleValueList = (IList<string>)possibleValues;

        if (possibleValueList.Count == 1)
        {
          result = possibleValueList[0];
        }
        else
        {
          // The max value used in 'Random.Next' is an exclusive bound so the count of items is sufficient.
          var randomChoice = RandomNumber.Next(0, possibleValueList.Count);
          result = possibleValueList[randomChoice];
        }
      }

      return result is string ? (string)result! : result.ToString()!;
    }
  }
}
