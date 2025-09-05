using DataGenerator.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace DataGenerator.Generators
{
  /// <summary>
  /// Represents a service for generating data for object properties using a fluent-like syntax.
  /// </summary>
  public class DataGenerator<T> : ComplexGeneratorBase<T> where T : class, new()
  {
    /// <summary>
    /// Represents a map of properties to value generator call routers.
    /// </summary>
    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal class PropertyCallRouterMap : Dictionary<PropertyInfo, ValueGeneratorCallRouter> { }

    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal readonly PropertyCallRouterMap _ValueGenerators = new PropertyCallRouterMap();

    /// <summary>
    /// Represents a map of properties to complex generators.
    /// </summary>
    internal class ComplexGeneratorMap : Dictionary<PropertyInfo, IComplexGenerator> { }

    /// <remarks>
    /// Internal for unit tests.
    /// </remarks>
    internal readonly ComplexGeneratorMap _ComplexGenerators = new ComplexGeneratorMap();

    private static PropertyInfo GetPropertyInfo<TResult>(Expression<Func<T, TResult?>> expression)
    {
      var propertyName = PropertyName.For(expression);
      return typeof(T).GetProperty(propertyName)!;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public DataGenerator() { }

    /// <summary>
    /// Applies all registered property generators to the given object.
    /// </summary>
    protected override void OnBeforeFinalize(T obj)
    {
      // Generate values except where a null is required.
      foreach (var entry in _ValueGenerators)
      {
        var property = entry.Key;
        var callRouter = entry.Value;

        // If the call router has been configured to generate a null value
        // (with some probability) then see if a null has been generated
        // and continue. Otherwise set a real value using a real method to call.
        if (callRouter.IsNullCall && callRouter.IsNullValueGenerated)
        {
          property.SetValue(obj, null);
          continue;
        }

        var propertyValue = callRouter.CallMethod(obj);
        property.SetValue(obj, propertyValue);
      }

      // Generate complex values.
      foreach (var entry in _ComplexGenerators)
      {
        var property = entry.Key;
        var generator = entry.Value;

        var generatorType = typeof(IComplexGenerator<>).MakeGenericType(property.PropertyType);
        var propertyValue = generatorType.GetMethod("Generate", new Type[] { })!.Invoke(generator, null);
        property.SetValue(obj, propertyValue);
      }
    }

    /// <summary>
    /// Returns a property value generator for the given property expression.
    /// </summary>
    public PropertyDataGenerator<T, TResult> For<TResult>(Expression<Func<T, TResult?>> property)
    {
      Guard.ArgumentNotNull(property, nameof(property));

      var propertyInfo = GetPropertyInfo(property);
      return new PropertyDataGenerator<T, TResult>(this, propertyInfo);
    }

    /// <summary>
    /// Registers a value generator and a method to call on it (with parameters) for a property.
    /// A probability of getting a null value can be specified.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator(
      PropertyInfo propertyInfo,
      IValueGenerator generator,
      string methodToCall,
      double nullProbability,
      params object[] parameters)
    {
      if (_ValueGenerators.ContainsKey(propertyInfo))
      {
        throw new InvalidOperationException(
          $"The property '{propertyInfo.Name}' already has a registered generator.");
      }

      _ValueGenerators.Add(
        propertyInfo,
        new ValueGeneratorCallRouter(generator, methodToCall, nullProbability, parameters));
    }

    /// <summary>
    /// Registers a value generator for a property.
    /// A probability of getting a null value can be specified.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator(PropertyInfo propertyInfo, IValueGenerator generator, double nullProbability)
    {
      _ValueGenerators.Add(propertyInfo, new ValueGeneratorCallRouter(generator, nullProbability));
    }

    /// <summary>
    /// Registers a value generator for a property expression.
    /// A probability of getting a null value can be specified.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator(Expression<Func<T, object>> expression, IValueGenerator generator, double nullProbability)
    {
      var propertyName = PropertyName.For(expression);
      var propertyInfo = typeof(T).GetProperty(propertyName)!;
      RegisterPropertyGenerator(propertyInfo, generator, nullProbability);
    }

    /// <summary>
    /// Registers a complex generator for a property expression.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator(Expression<Func<T, object>> expression, IComplexGenerator generator)
    {
      var propertyName = PropertyName.For(expression);
      var propertyInfo = typeof(T).GetProperty(propertyName)!;
      RegisterPropertyGenerator(propertyInfo, generator);
    }

    /// <summary>
    /// Registers a complex generator for a property.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator(PropertyInfo propertyInfo, IComplexGenerator generator)
    {
      _ComplexGenerators.Add(propertyInfo, generator);
    }

    /// <summary>
    /// Registers a complex generator for a property with a finalize action.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator<TOther>(PropertyInfo propertyInfo, IComplexGenerator<TOther> generator, Action<TOther> finalizeAction)
        where TOther : class, new()
    {
      _ComplexGenerators.Add(propertyInfo, generator);
      generator.Finalize += new GeneratorFinalizer<TOther>(finalizeAction);
    }

    /// <summary>
    /// Registers a complex generator for a property expression with a finalize action.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="PropertyDataGenerator{T,TResult}"/> which checks all parameters so we don't have to.
    /// </remarks>
    internal void RegisterPropertyGenerator<TOther>(Expression<Func<T, TOther>> expression, IComplexGenerator<TOther> generator, Action<TOther> finalizeAction)
        where TOther : class, new()
    {
      var propertyName = PropertyName.For(expression);
      var propertyInfo = typeof(T).GetProperty(propertyName)!;
      RegisterPropertyGenerator(propertyInfo, generator, finalizeAction);
    }
  }
}
