using DataGenerator.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace DataGenerator.Core
{
  /// <summary>
  /// Represents a helper class to correctly route a call to a value generator.
  /// </summary>
  /// <remarks>
  /// It can invoke a given method on an <see cref="IValueGenerator"/>
  /// and unwrap the method's parameters if they were specified as a lambda expression.
  /// </remarks>
  internal class ValueGeneratorCallRouter
  {
    private static readonly Lazy<Random> RandomNumber = new Lazy<Random>(true);

    private readonly IValueGenerator _ValueGenerator;
    private readonly MethodInfo _MethodToCall;
    private readonly object[] _MethodParameters;
    private readonly double _ProbabilityOfNull;


    internal IValueGenerator Generator => _ValueGenerator;
    internal MethodInfo MethodToCall => _MethodToCall;
    internal object[] MethodParameters => _MethodParameters;
    internal double ProbabilityOfNull => _ProbabilityOfNull;

    /// <summary>
    /// Initializes this instance with a generator and a probability of getting a null value.
    /// </summary>
    public ValueGeneratorCallRouter(IValueGenerator generator, double nullProbability)
        : this(generator, "New", nullProbability) { }

    /// <summary>
    /// Initializes this instance with a generator, a method to call and a probability of getting a null value.
    /// </summary>
    public ValueGeneratorCallRouter(IValueGenerator generator, string methodName, double nullProbability)
    {
      Guard.ArgumentNotNull(generator, nameof(generator));
      Guard.ArgumentNotNullOrEmpty(methodName, nameof(methodName));
      Guard.ArgumentInRange(0.0, 1.0, nullProbability, nameof(nullProbability));

      if (string.IsNullOrWhiteSpace(methodName))
      {
        throw new ArgumentException("Method name cannot be empty", nameof(methodName));
      }

      _ValueGenerator = generator;
      _MethodToCall = generator.GetType().GetMethod(methodName)!;
      _ProbabilityOfNull = nullProbability;
      _MethodParameters = null!;
    }

    /// <summary>
    /// Initializes this instance with a generator, a method to call, its parameters and a probability of getting a null value.
    /// </summary>
    public ValueGeneratorCallRouter(IValueGenerator generator, string methodName, double nullProbability, params object[] parameters)
        : this(generator, methodName, nullProbability)
    {
      Guard.ArgumentNotNull(parameters, nameof(parameters));

      _MethodParameters = parameters;
    }

    /// <summary>
    /// Calls the given method on the generator. If any parameters to the method
    /// were wrapped (subclass of <see cref="LambdaExpression"/> then we unwrap (and resolve) them.
    /// </summary>
    public object? CallMethod<T>(T builtObject) where T : class, new()
    {
      Guard.ArgumentNotNull(builtObject, nameof(builtObject));

      if (_MethodParameters is null)
      {
        return _MethodToCall.Invoke(_ValueGenerator, null);
      }

      var unwrappedParameters = new List<object>();

      foreach (var wrappedParam in _MethodParameters)
      {
        var wrappedParameterType = wrappedParam.GetType();

        if (wrappedParameterType.IsSubclassOf(typeof(LambdaExpression)))
        {
          var wrappedLambda = (LambdaExpression)wrappedParam;
          var unwrappedParameter = wrappedLambda.Compile().DynamicInvoke(builtObject);
          unwrappedParameters.Add(unwrappedParameter!);
        }
        else
        {
          unwrappedParameters.Add(wrappedParam);
        }
      }

      return _MethodToCall.Invoke(_ValueGenerator, unwrappedParameters.ToArray());
    }

    /// <summary>
    /// Returns true if a null value has been generated with probability given by <see cref="_ProbabilityOfNull"/>.
    /// </summary>
    public bool IsNullValueGenerated
    {
      get { return RandomNumber.Value.IsNextNull(_ProbabilityOfNull); }
    }

    /// <summary>
    /// Returns true if this call router has been configured to return a null with a certain probability.
    /// </summary>
    public bool IsNullCall
    {
      get { return _ProbabilityOfNull > 0.0; }
    }
  }
}
